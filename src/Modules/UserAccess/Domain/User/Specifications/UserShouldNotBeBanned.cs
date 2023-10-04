using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class UserShouldNotBeBanned : IBussinessRuleSpecification
    {
        public string Message => "user banned";
        bool _ban;
        public Enum Code => throw new NotImplementedException();
        public UserShouldNotBeBanned(bool ban)
        {
            _ban = ban;
        }
        public bool isSatisfiedBy()
        {
            return !_ban;
        }
    }
}
