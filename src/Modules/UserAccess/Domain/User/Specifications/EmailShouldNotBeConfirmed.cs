using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class EmailShouldNotBeConfirmed : IBussinessRuleSpecification
    {
        bool _isEmailActive;
        string Message;


        public Enum Code => BusinessRuleErrorEnum.EmailShouldNotBeConfirmed;

        string IBussinessRuleSpecification.Message => Message;

        public EmailShouldNotBeConfirmed(bool isEmailActive, string message
            )
        {
            _isEmailActive = isEmailActive;
            Message = message;
        }

        public bool isSatisfiedBy()
        {
            return !_isEmailActive;
        }
    }
}
