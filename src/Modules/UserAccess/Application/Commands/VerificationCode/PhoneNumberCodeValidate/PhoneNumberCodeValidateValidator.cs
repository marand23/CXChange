using CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeValidate;
using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.PhoneNumberCodeValidate
{
    public class ValidatePhoneNumberCodeValidator : AbstractValidator<ValidatePhoneNumberCode>
    {
        public ValidatePhoneNumberCodeValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Code).VerificationCode();

        }
    }
}
