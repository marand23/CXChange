using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class UserKycShouldBePossibleOnKycInitialazation : IBussinessRuleSpecification
    {
        public string Message => "kyc for this user is not possible or restricted";

        public Enum Code => BusinessRuleErrorEnum.KycVerificationIsNotPossible;
        KycStatus _status;
        public UserKycShouldBePossibleOnKycInitialazation(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return !_status.Equals(KycStatus.Denied);
        }
    }
}
