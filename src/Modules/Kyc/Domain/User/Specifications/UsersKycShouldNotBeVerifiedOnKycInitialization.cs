using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class UsersKycShouldNotBeVerifiedOnKycInitialization : IBussinessRuleSpecification
    {
        public string Message => "user kyc is already verified";

        public Enum Code => BusinessRuleErrorEnum.KycAlreadyVerified;
        KycStatus _status;
        public UsersKycShouldNotBeVerifiedOnKycInitialization(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return !(_status == KycStatus.Verified);
        }
    }
}
