using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.UserAccess.Infrastructure.CustomUserDataProtectionProviders
{
    internal class RegisterTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public RegisterTokenProviderOptions()
        {
            Name = "RegisterTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(10);
        }
    }
    internal class RegisterTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public RegisterTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<LoginTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }
}

