using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordChange
{
    public class ChangePasswordValidator : AbstractValidator<ChangePassword>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).Password();
            RuleFor(x => x.OldPassword).Password();
            RuleFor(x => x.Token).UserToken();
        }
    }
}
