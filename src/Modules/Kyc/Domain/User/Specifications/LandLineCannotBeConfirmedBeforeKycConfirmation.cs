using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class LandLineCannotBeConfirmedBeforeKycConfirmation : IBussinessRuleSpecification
    {
        public string Message => "landline cannot be confired before kyc confirmation";

        public Enum Code => BusinessRuleErrorEnum.LandLineCannotConfirmedBeforeKyc;
        KycStatus _status;
        public LandLineCannotBeConfirmedBeforeKycConfirmation(KycStatus status)
        {
            _status = status;
        }
        public bool isSatisfiedBy()
        {
            return _status == KycStatus.Verified;
        }
    }
}
