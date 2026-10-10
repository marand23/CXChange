using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Payment.Domain.Deposit.Enums;

namespace CXChange.Modules.Payment.Domain.Deposit.Specifications
{
    internal class IRTDepositShouldBEInProgressOnDepositRejection : IBussinessRuleSpecification
    {
        public string Message => "deposit should be in progress to be rejected";

        public Enum Code => BusinessRuleErrorEnum.IRTDepositNotInProgressOnRejection;
        DepositStatus _status;
        public IRTDepositShouldBEInProgressOnDepositRejection(DepositStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return _status == DepositStatus.InProgress;
        }
    }
}
