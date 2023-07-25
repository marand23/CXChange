using CXChange.BuildingBlocks.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UsersPhoneNumberShouldNotBeConfirmedOnRegistration : IBussinessRuleSpecification
    {
        private bool PhoneNumberConfirmed;
        public UsersPhoneNumberShouldNotBeConfirmedOnRegistration(bool PhoneNumberConfirmed) { 
            this.PhoneNumberConfirmed = PhoneNumberConfirmed;
        }
        public string Message => "User Already exists";

        public bool isSatisfiedBy()
        {
            return !PhoneNumberConfirmed;
        }
    }
}
