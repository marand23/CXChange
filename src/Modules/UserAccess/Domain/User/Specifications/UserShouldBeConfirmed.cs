using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShouldBeConfirmed : IBussinessRuleSpecification
    {
        private bool _isConfirmed;
        public UserShouldBeConfirmed(bool isConfirmed)
        {
            _isConfirmed = isConfirmed;
        }

        public string Message => "user did not confirmed yet";

        public Enum Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return _isConfirmed;
        }
    }
}
