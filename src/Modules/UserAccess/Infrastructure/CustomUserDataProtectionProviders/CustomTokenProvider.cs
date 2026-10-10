using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CXChange.Modules.UserAccess.Infrastructure.CustomUserDataProtectionProviders
{
    internal class CustomTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public CustomTokenProviderOptions()
        {
            Name = "CustomTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(10);
        }
    }
    internal class CustomTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public CustomTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<CustomTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
