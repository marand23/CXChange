using CXChange.Modules.UserAccess.Domain.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CXChange.Modules.UserAccess.Infrastructure.DataModel
{
    public class AppUser : IdentityUser
    {

        public TwoFactorEnum ActiveTwoFactor { get; set; }
        public bool IsAuthenticatorEnabled { get; set; }
        public string[] Tags { get; set; }
        [Column(TypeName = "ipaddress")]
        public string Address { get; set; }



    }

}
