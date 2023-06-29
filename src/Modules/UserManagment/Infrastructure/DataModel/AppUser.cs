
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Infrastructure.DataModel
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string DefaultTwoFactor { get; set; }
        public ICollection<AuthenticationTickets> UserTickets { get; set; }


    }
}
