using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.UserAccess.Infrastructure.CustomUserDataProtectionProviders
{
    internal class LoginTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public LoginTokenProviderOptions()
        {
            Name = "LoginTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(10);
        }
    }
    internal class LoginTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public LoginTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<LoginTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
