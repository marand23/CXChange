using CXChange.Modules.UserAccess.Application.Utils;
using Microsoft.AspNetCore.Authorization;

namespace CXChange.Modules.UserAccess.Infrastructure.Authorization.CodeTypeAuthorization
{
    public class CodeRequirement : IAuthorizationRequirement
    {

        public Func<string, CodeType> _func { get; }

        public CodeRequirement(Func<string, CodeType> func)
        {

            _func = func;
        }
    }
}
