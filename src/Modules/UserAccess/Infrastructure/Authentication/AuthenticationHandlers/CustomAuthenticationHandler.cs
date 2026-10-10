using CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore;
using CXChange.Modules.UserManagment.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CXChange.Modules.UserAccess.Infrastructure.Authentication.AuthenticationHandlers
{
    internal class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public const string SchemeName = "CustomScheme";


        public TimeSpan ExpireTimeSpan { get; set; }
        /// <summary>
        /// storage for saving user authentication tokens in database or redis
        /// </summary>
        public ICustomTicketStore SessionStore { get; set; }
        public bool isExtendable { get; set; }
        // public RandomTokenProvider UserTokenGenerator { get; set; }

    }
    internal class CustomAuthenticationHandler : SignInAuthenticationHandler<CustomAuthOptions>
    {

        string token = null;
        string _sessionKey = string.Empty;

        private const string SessionIdClaim = "Microsoft.AspNetCore.Authentication.Cookies-SessionId";
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
       : base(options, logger, encoder, clock) { }
        //signoutasync for customscheme
        protected override Task HandleSignOutAsync(AuthenticationProperties properties)
        {
            if (!String.IsNullOrEmpty(Request.Headers["token"]))
            {
                Options.SessionStore.RemoveAsync(Request.Headers["token"]);
            }


            return Task.FromResult(0);
        }
        //signinasyn for customscheme
        protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {


            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            DateTimeOffset issuedUTC;
            //we dont provide properties in signinasync,here we will fill properties with option that we have
            properties = properties ?? new AuthenticationProperties();

            if (properties.IssuedUtc.HasValue)
            {
                issuedUTC = properties.IssuedUtc.Value;
            }
            else
            {
                issuedUTC = Clock.UtcNow;
                properties.IssuedUtc = issuedUTC;
            }
            //if we have not expiretime we will provide that with options
            if (!properties.ExpiresUtc.HasValue)
            {
                properties.ExpiresUtc = issuedUTC.Add(Options.ExpireTimeSpan);
            }
            //create authenication ticket
            var ticket = new AuthenticationTicket(user, properties, "CustomScheme");

            _sessionKey = Request.Headers["Authorizatoin"];
            if (_sessionKey != null)
            {
                await Options.SessionStore.RenewAsync(_sessionKey.ComputeSha256Hash());
            }
            else
            {
                _sessionKey = await Options.SessionStore.StoreAsync(ticket);
            }
            var principal = new ClaimsPrincipal(
                   new ClaimsIdentity(
                       new[] { new Claim(SessionIdClaim, _sessionKey, ClaimValueTypes.String, Options.ClaimsIssuer) },
                       Options.ClaimsIssuer));

            var authProperties = new AuthenticationProperties();
            authProperties.SetParameter("token", _sessionKey);
            //put token in context items to get it after sign in 
            ticket = new AuthenticationTicket(principal, authProperties, "CustomScheme");


            Context.Items.Add("token", _sessionKey);

        }
        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {

            Response.StatusCode = StatusCodes.Status401Unauthorized;


            Response.Headers.Append(HeaderNames.WWWAuthenticate, "CustomScheme");


            Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new { code = 401, message = "unathorized" });
            return Response.WriteAsync(result);
        }
        protected override Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            var result = JsonConvert.SerializeObject(new { code = 403, message = "forbidden" });
            return Response.WriteAsync(result);
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            string token = Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(token))
            {

                return AuthenticateResult.NoResult();

            }


            //retrieve ticket from database
            var ticket = await Options.SessionStore.RetrieveAsync(token.ComputeSha256Hash());
            if (ticket == null)
            {
                return AuthenticateResult.Fail("Ticket is not valid");
            }
            //check if ticket has valid expiration date 
            var currentUtc = Clock.UtcNow;
            var expireUtc = ticket.Properties.ExpiresUtc;
            if (expireUtc != null && expireUtc.Value < currentUtc)
            {
                await Options.SessionStore.RemoveAsync(token.ComputeSha256Hash());
                return AuthenticateResult.Fail("Ticket  has been expired");
            }
            //if everything is ok return the ticket
            return AuthenticateResult.Success(ticket);


        }
    }
}
