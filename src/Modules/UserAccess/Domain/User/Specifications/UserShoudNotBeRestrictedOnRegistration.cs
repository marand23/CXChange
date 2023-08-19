using CXChange.BuildingBlocks.Domain.Specifications;

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

        public int Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return !isRestricted;
        }
    }
}
