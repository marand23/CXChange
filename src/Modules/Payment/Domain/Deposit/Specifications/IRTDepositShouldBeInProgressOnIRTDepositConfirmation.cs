using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Deposit.Enums;

namespace CXChange.Modules.Payment.Domain.Deposit.Specifications
{
    internal class IRTDepositShouldBeInProgressOnIRTDepositConfirmation : IBussinessRuleSpecification
    {
        public string Message => "irt deposit was not in progress state on deposit confirmation";

        public Enum Code => BusinessRuleErrorEnum.IRTDepositShouldBeInProgressState;
        DepositStatus _status;
        public IRTDepositShouldBeInProgressOnIRTDepositConfirmation(DepositStatus status)
        {
            _status = status;
        }

        public bool isSatisfiedBy()
        {
            return _status.Equals(DepositStatus.InProgress);
        }
    }
}
