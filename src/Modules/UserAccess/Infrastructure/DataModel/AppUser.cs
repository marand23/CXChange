using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.DataModel
{
    public class AppUser : IdentityUser
    {

        public string DefaultTwoFactor { get; set; }
        public bool IsRestricted { get; set; }
        public ICollection<AuthenticationTickets> UserTickets { get; set; }


    }
}
