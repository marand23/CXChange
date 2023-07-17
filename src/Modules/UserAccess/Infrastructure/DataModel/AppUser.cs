
using CXChange.BuildingBlocks.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.DataModel
{
    public class AppUser : IdentityUser
    {
      
        public string DefaultTwoFactor { get; set; }
        public bool IsRestricted { get; set; }
        public ICollection<AuthenticationTickets> UserTickets { get; set; }


    }
}
