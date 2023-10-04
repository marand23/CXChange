using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeValidate
{
    public class ValidateEmailCodeValidator : AbstractValidator<ValidateEmailCode>
    {
        public ValidateEmailCodeValidator()
        {
            RuleFor(x => x.Code).VerificationCode();

        }
    }
}
