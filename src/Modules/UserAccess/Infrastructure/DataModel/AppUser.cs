using CXChange.Modules.UserAccess.Domain.User;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.DataModel
{
    public class AppUser : IdentityUser
    {

        public TwoFactorEnum ActiveTwoFactor { get; set; }
        public bool IsAuthenticatorEnabled { get; set; }



    }

}
