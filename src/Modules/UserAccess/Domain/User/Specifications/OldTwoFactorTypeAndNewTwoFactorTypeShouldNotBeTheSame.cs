using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class OldTwoFactorTypeAndNewTwoFactorTypeShouldNotBeTheSame : IBussinessRuleSpecification
    {
        private TwoFactorEnum _old;
        private TwoFactorEnum _new;
        public OldTwoFactorTypeAndNewTwoFactorTypeShouldNotBeTheSame(TwoFactorEnum oldType, TwoFactorEnum newType)
        {
            _old = oldType;
            _new = newType;
        }
        public string Message => "new type should be different";

        public Enum Code => throw new NotImplementedException();

        public bool isSatisfiedBy()
        {
            return !(_old == _new);
        }
    }
}
