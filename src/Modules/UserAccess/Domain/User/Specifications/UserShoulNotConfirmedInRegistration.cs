using CXChange.BuildingBlocks.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShoulNotConfirmedInRegistration : IBussinessRuleSpecification
    {
        private bool PhoneNumberConfirmed;
        public UserShoulNotConfirmedInRegistration(bool PhoneNumberConfirmed) { 
            this.PhoneNumberConfirmed = PhoneNumberConfirmed;
        }
        public string Message => "User Already Confirmed";

        public bool isSatisfiedBy()
        {
            return true;
        }
    }
}
