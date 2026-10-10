using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Withdraw.Enums;

namespace CXChange.Modules.Payment.Domain.Withdraw.Specifications
{
    internal class WithdrawStatusShouldBeConfirmedByUseronSendingWithdraw : IBussinessRuleSpecification
    {
        public string Message => "tried to send withdraw but the status was not  confirmed by user";

        public Enum Code => BusinessRuleErrorEnum.WithdrawShouldBeConfirmedByUserOnSend;
        WithdrawStatus _status;
        public WithdrawStatusShouldBeConfirmedByUseronSendingWithdraw(WithdrawStatus status)
        {
            _status = status;
        }

        public bool isSatisfiedBy()
        {
            return _status == WithdrawStatus.ConfirmedByUser;
        }
    }
}
