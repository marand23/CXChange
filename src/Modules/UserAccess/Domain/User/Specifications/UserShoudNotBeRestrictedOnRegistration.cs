using CXChange.BuildingBlocks.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShoudNotBeRestrictedOnRegistration : IBussinessRuleSpecification
    {
        bool isRestricted;
        public UserShoudNotBeRestrictedOnRegistration(bool isRestricted)
        {
            this.isRestricted = isRestricted;
        }

        public string Message => "user restricted";

        public bool isSatisfiedBy()
        {
            return !isRestricted;
        }
    }
}
