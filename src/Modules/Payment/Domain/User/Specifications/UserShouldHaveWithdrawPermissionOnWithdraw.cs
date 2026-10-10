using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.Payment.Domain.User.Specifications
{
    internal class UserShouldHaveWithdrawPermissionOnWithdraw : IBussinessRuleSpecification
    {
        public string Message => "user does not have permission for withdraw";

        public Enum Code => BusinessRuleErrorEnum.UserDoesNotHaveWithdrawPermission;
        bool _hasPermission;
        public UserShouldHaveWithdrawPermissionOnWithdraw(bool hasPermission)
        {
            _hasPermission = hasPermission;
        }
        public bool isSatisfiedBy()
        {
            return _hasPermission;
        }
    }
}
