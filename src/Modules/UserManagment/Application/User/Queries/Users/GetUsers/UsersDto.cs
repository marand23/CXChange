using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Application.User.Queries.Users.GetUsers
{
    public class UsersDto
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumberConfirmed { get; set; }
        public string EmailConfirmed { get; set; }
        public string TwoFactorEnabled { get; set; }
        public string TwoFactorType { get; set; }
    }
}
