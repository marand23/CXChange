using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend
{
    public class SendCodeToEmailValidator : AbstractValidator<SendCodeToEmail>
    {
        public SendCodeToEmailValidator()
        {
            RuleFor(x => x.Email).EmailAddress();

        }
    }
}
