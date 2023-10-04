using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode
{
    public class VerifyResetPasswordCodeValidator : AbstractValidator<VerifyResetPasswordCode>
    {
        public VerifyResetPasswordCodeValidator()
        {
            RuleFor(x => x.VerificationCode).VerificationCode();
            RuleFor(x => x.PhoneNumber).PhoneNumber();
        }
    }
}
