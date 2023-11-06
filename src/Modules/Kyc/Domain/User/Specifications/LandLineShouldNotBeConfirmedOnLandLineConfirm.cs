using CXChange.BuildingBlocks.Domain.Specifications;
using CXChange.Modules.Kyc.Domain.User.Enums;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class LandLineShouldNotBeConfirmedOnLandLineConfirm : IBussinessRuleSpecification
    {
        public string Message => "landline is already confirmed";

        public Enum Code => BusinessRuleErrorEnum.LandLineAlreadyConfirmed;
        LandLineStatus _status;
        public LandLineShouldNotBeConfirmedOnLandLineConfirm(LandLineStatus status) { _status = status; }
        public bool isSatisfiedBy()
        {
            return _status == LandLineStatus.UnVerified;
        }
    }
}
