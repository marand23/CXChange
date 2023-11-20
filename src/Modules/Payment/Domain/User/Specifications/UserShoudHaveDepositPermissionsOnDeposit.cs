using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.Payment.Domain.User.Specifications
{
    internal class UserShouldHaveDepositPermissionsOnDeposit : IBussinessRuleSpecification
    {
        public string Message => "user does not have deposit permissions";

        public Enum Code => BusinessRuleErrorEnum.UserDoesNotHaveDepositPermissions;
        public UserShouldHaveDepositPermissionsOnDeposit(bool canDeposit)
        {
            _canDeposit = canDeposit;
        }
        bool _canDeposit;
        public bool isSatisfiedBy()
        {
            return _canDeposit;
        }
    }
}
