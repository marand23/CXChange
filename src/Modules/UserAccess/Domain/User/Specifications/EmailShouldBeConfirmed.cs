using CXChange.BuildingBlocks.Domain.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User.Specifications
{
    internal class EmailShouldBeConfirmed : IBussinessRuleSpecification
    {
        bool _isConfirmed;
        string Message;

        public Enum Code => BusinessRuleErrorEnum.EmailNotConfirmed;

        string IBussinessRuleSpecification.Message => Message;

        public EmailShouldBeConfirmed(bool isConfirmed, string message)
        {
            _isConfirmed = isConfirmed;
            Message = message;
        }
        public bool isSatisfiedBy()
        {
            return _isConfirmed;
        }
    }
}
