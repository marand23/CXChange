using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.Kyc.Domain.User.Specifications
{
    internal class NationalIdShouldBeUnique : IBussinessRuleSpecification
    {
        public string Message => "national id is not unique";

        public Enum Code => BusinessRuleErrorEnum.NationalIdIsNotUnique;
        public bool _isUnique;
        public NationalIdShouldBeUnique(bool isUnique)
        {
            _isUnique = isUnique;
        }
        public bool isSatisfiedBy()
        {
            return _isUnique;
        }
    }
}
