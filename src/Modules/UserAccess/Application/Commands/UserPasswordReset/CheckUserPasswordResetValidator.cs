using CXChange.Modules.UserAccess.Application.Commands.UserPasswordReset;
using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordChangeCheck
{
    internal class CheckUserPasswordResetValidator : AbstractValidator<CheckUserPasswordReset>
    {
        public CheckUserPasswordResetValidator()
        {
            RuleFor(X => X.PhoneNumber).PhoneNumber();
        }
    }
}
