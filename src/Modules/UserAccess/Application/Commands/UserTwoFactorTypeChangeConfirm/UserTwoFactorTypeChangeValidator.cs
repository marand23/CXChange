using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm
{
    internal class ConfirmUserTwoFactorTypeChangeValidator : AbstractValidator<ConfirmUserTwoFactorTypeChange>
    {
        public ConfirmUserTwoFactorTypeChangeValidator()
        {
            RuleFor(x => x.UserId).UserId();
            //RuleFor(x => x.NewTwoFactorType).TwoFactorType();
        }
    }
}
