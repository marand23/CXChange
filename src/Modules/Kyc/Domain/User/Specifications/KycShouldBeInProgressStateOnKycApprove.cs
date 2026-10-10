using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class KycShouldBeInProgressStateOnKycApprove : IBussinessRuleSpecification
    {
        public string Message => "kyc is not in in-progress state on kyc approval";

        public Enum Code => BusinessRuleErrorEnum.KycIsNotInProgressOnApproval;
        public KycStatus _status;
        public KycShouldBeInProgressStateOnKycApprove(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return _status == KycStatus.InProgress;
        }
    }
}
