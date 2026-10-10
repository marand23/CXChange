using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class UserKycShouldNotBeInProgressOnKycInitialization : IBussinessRuleSpecification
    {
        public string Message => "kyc verification is in progress";

        public Enum Code => BusinessRuleErrorEnum.KycVerificationInProgress;
        KycStatus _status;
        public UserKycShouldNotBeInProgressOnKycInitialization(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return !_status.Equals(KycStatus.InProgress);
        }
    }
}
