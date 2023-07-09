using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User
{
    public class SnapShot
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string DefaultTwoFactor { get; set; }
        public string UserName { get; set; }
    }
}
