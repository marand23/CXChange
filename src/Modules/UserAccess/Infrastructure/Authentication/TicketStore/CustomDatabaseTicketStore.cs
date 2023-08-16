using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserManagment.Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CXChange.Modules.UserAccess.Infrastructure.Authentication.TicketStore
{
    internal class CustomDatabaseTicketStore : ICustomTicketStore
    {
        private readonly IServiceCollection _services;
        public CustomDatabaseTicketStore(IServiceCollection services)
        {
            _services = services;
        }
        public async Task<AuthenticationTicket> RetrieveAsync(string key)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserManagementDbContext>();


                var authenticationTicket = await context.AuthenticationTickets.FindAsync(key);
                if (authenticationTicket != null)
                {
                    authenticationTicket.LastActivity = DateTimeOffset.UtcNow;
                    await context.SaveChangesAsync();

                    return DeserializeFromBytes(authenticationTicket.Value);
                }

            }

            return null;
        }
        public async Task RemoveAsync(string key)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserManagementDbContext>();

                var ticket = await context.AuthenticationTickets.SingleOrDefaultAsync(x => x.Id == key);
                if (ticket != null)
                {
                    context.AuthenticationTickets.Remove(ticket);
                    await context.SaveChangesAsync();
                }

            }
        }
        public async Task RenewAsync(string key)
        {
            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserManagementDbContext>();



                var authenticationTicket = await context.AuthenticationTickets.FindAsync(key);
                if (authenticationTicket != null)
                {
                    //  authenticationTicket.Value = SerializeToBytes(ticket);
                    authenticationTicket.LastActivity = DateTimeOffset.UtcNow;
                    //authenticationTicket.Expires = ticket.Properties.ExpiresUtc;
                    await context.SaveChangesAsync();
                }

            }
        }
        public async Task<string> StoreAsync(AuthenticationTicket ticket)
        {
            var token = string.Empty;
            var userId = string.Empty;
            var nameIdentifier = ticket.Principal.Identity.Name;

            using (var scope = _services.BuildServiceProvider().CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserManagementDbContext>();

                if (ticket.AuthenticationScheme == "Identity.Application")
                {
                    userId = nameIdentifier;

                }
                else if (ticket.AuthenticationScheme == "CustomScheme")
                {

                    userId = (await context.Users.SingleAsync(x => x.UserName == nameIdentifier)).Id;

                }
                // If using a external login provider like google we need to resolve the userid through the Userlogins
                else if (ticket.AuthenticationScheme == "Identity.External")
                {
                    userId = (await context.UserLogins.SingleAsync(x => x.ProviderKey == nameIdentifier)).UserId;
                }
                token = ExtensioinMethods.GenerateRandomToken(64);
                var authenticationTicket = new AuthenticationTickets()
                {
                    Id = token.ComputeSha256Hash(),
                    UserId = userId,
                    LastActivity = DateTimeOffset.UtcNow,
                    Value = SerializeToBytes(ticket),
                };

                var expiresUtc = ticket.Properties.ExpiresUtc;
                if (expiresUtc.HasValue)
                {
                    authenticationTicket.Expires = expiresUtc.Value;
                }
                var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
                var httpContext = httpContextAccessor?.HttpContext;
                if (httpContext != null)
                {
                    var remoteAddress = httpContext.Connection.RemoteIpAddress.ToString();
                    if (remoteAddress != null) { authenticationTicket.RemoteIpAddress = remoteAddress; }
                }
                var userAgent = httpContext.Request.Headers["User-Agent"];
                if (!string.IsNullOrEmpty(userAgent))
                {
                    var uaParser = UAParser.Parser.GetDefault();
                    var clientInfo = uaParser.Parse(userAgent);
                    authenticationTicket.OS = clientInfo.OS.ToString();
                    authenticationTicket.UserAgentFamily = clientInfo.UserAgent.Family;
                    authenticationTicket.UserAgenetVersion = $"{clientInfo.UserAgent.Major}.{clientInfo.UserAgent.Minor}.{clientInfo.UserAgent.Patch}";
                    authenticationTicket.DeviceId = "device";

                }
                //send email to client about login with new device
                await context.AuthenticationTickets.AddAsync(authenticationTicket);

                await context.SaveChangesAsync();
                //  var a = authenticationTicket.Id.ToString();
                return token;
            }
        }
        public async Task RemoveAllAsync()
        {
            await Task.FromResult(0);
        }
        private static byte[] SerializeToBytes(AuthenticationTicket source)
        {
            return TicketSerializer.Default.Serialize(source);
        }

        private static AuthenticationTicket DeserializeFromBytes(byte[] source)
        {
            return source == null ? null : TicketSerializer.Default.Deserialize(source);
        }
    }
}
