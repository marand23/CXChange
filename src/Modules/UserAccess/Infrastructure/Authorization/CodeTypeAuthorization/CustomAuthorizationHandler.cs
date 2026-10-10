using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.UserAccess.Infrastructure.Authorization.CodeTypeAuthorization
{
    public class CodeTypeAuthorizationHandler : AuthorizationHandler<CodeRequirement>
    {
        private IHttpContextAccessor _contextAccessor;
        public CodeTypeAuthorizationHandler(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CodeRequirement requirement)
        {

            HttpContext httpContext = _contextAccessor.HttpContext;
            var codeType = httpContext.Request.Query["codeType"].ToString();
            if (string.IsNullOrEmpty(codeType) || requirement._func(codeType) == null)
            {

                context.Fail();
                return Task.CompletedTask;
            }
            var requireAuthentication = requirement._func(codeType).RequiresAuthentication;
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (requireAuthentication && !isAuthenticated)
            {
                context.Fail();
                return Task.CompletedTask;
            }
            context.Succeed(requirement);
            return Task.CompletedTask;


        }
    }

}



