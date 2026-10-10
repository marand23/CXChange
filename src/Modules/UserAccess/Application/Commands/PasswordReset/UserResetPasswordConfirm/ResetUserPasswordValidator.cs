using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm
{
    public class ResetUserPasswordValidator : AbstractValidator<ResetUserPasswordConfirm>
    {
        public ResetUserPasswordValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.NewPassword).Password();

        }
    }
}
