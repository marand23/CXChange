using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShoudNotBeRestricted : IBussinessRuleSpecification
    {
        bool isRestricted;
        public UserShoudNotBeRestricted(bool isRestricted)
        {
            this.isRestricted = isRestricted;
        }

        public string Message => "user restricted";

        public Enum Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return !isRestricted;
        }
    }
}
