using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class EmailShouldNotBeActiveOnEmailActivation : IBussinessRuleSpecification
    {
        bool _isEmailActive;
        public string Message => "email is already active";

        public Enum Code => throw new NotImplementedException();
        public EmailShouldNotBeActiveOnEmailActivation(bool isEmailActive
            )
        {
            _isEmailActive = isEmailActive;
        }

        public bool isSatisfiedBy()
        {
            return !_isEmailActive;
        }
    }
}
