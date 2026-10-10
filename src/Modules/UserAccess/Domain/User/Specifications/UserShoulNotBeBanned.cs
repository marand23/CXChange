using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShoulNotBeBanned : IBussinessRuleSpecification
    {
        private bool _isBanned;
        public UserShoulNotBeBanned(bool isBanned)
        {
            _isBanned = isBanned;
        }
        public string Message => "user banned";

        public Enum Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return !_isBanned;
        }
    }
}
