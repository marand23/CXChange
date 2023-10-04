using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate
{
    public class ValidateAuthenticatorCodeValidator : AbstractValidator<ValidateAuthenticatorCode>
    {
        public ValidateAuthenticatorCodeValidator()
        {
            RuleFor(x => x.Code).VerificationCode();

        }
    }
}
