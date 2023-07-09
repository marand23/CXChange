using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.Exceptions
{
    [Serializable]
    public class UserRegistrationException : Exception
    {
        public string PhoneNumber { get; }

        public UserRegistrationException() { }

        public UserRegistrationException(string message)
            : base(message) { }

        public UserRegistrationException(string message, Exception inner)
            : base(message, inner) { }

        public UserRegistrationException(string message, string phoneNumber)
            : this(message)
        {
            PhoneNumber=phoneNumber ;
        }
    }
}
