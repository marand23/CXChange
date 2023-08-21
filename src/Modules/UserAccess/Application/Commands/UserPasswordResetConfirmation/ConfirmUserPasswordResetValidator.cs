using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordResetConfirmation
{
    internal class ConfirmUserPasswordResetValidator : AbstractValidator<ConfirmUserPasswordReset>
    {
        public ConfirmUserPasswordResetValidator()
        {
            RuleFor(x => x.Token).UserToken();
            RuleFor(x => x.NewPassword).Password();
            RuleFor(X => X.VerificationCode).VerificationCode();
            RuleFor(x => x.PhoneNumber).PhoneNumber();
        }
    }
}
