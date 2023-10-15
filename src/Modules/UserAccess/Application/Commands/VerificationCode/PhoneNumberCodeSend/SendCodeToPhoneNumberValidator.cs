using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeSend
{
    public class SendCodeToPhoneNumberValidator : AbstractValidator<SendCodeToPhoneNumber>
    {
        public SendCodeToPhoneNumberValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();

        }
    }
}
