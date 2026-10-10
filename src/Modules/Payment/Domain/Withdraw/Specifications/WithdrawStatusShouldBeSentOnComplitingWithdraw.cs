using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Withdraw.Enums;

namespace CXChange.Modules.Payment.Domain.Withdraw.Specifications
{
    internal class WithdrawStatusShouldBeSentOnComplitingWithdraw : IBussinessRuleSpecification
    {
        public string Message => "tried to complete withdraw but the withdraw status was not sent";

        public Enum Code => BusinessRuleErrorEnum.WithdrawShouldBeSentOnComplete;
        WithdrawStatus _status;
        public WithdrawStatusShouldBeSentOnComplitingWithdraw(WithdrawStatus status)
        {
            _status = status;
        }

        public bool isSatisfiedBy()
        {
            return _status == WithdrawStatus.Sent;
        }
    }
}
