using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Integration.IntegrationEvents
{
    public class UserRegistrationConfirmed
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
