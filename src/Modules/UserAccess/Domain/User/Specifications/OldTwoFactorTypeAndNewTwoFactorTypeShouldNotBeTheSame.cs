using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class OldTwoFactorTypeAndNewTwoFactorTypeShouldNotBeTheSame : IBussinessRuleSpecification
    {
        private string _oldType;
        private string _newType;
        public OldTwoFactorTypeAndNewTwoFactorTypeShouldNotBeTheSame(string oldType, string newType)
        {
            _oldType = oldType;
            _newType = newType;
        }
        public string Message => "new type should be different";

        public Enum Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return !_oldType.Equals(_newType);
        }
    }
}
