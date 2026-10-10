using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class KycStatusShouldBeInProgressOnRejection : IBussinessRuleSpecification
    {
        public string Message => "kyc already approved and cannot be rejected";

        public Enum Code => BusinessRuleErrorEnum.KycAlreadyApprovedOnRejection;
        KycStatus _status;
        public KycStatusShouldBeInProgressOnRejection(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return _status == KycStatus.InProgress;
        }
    }
}
