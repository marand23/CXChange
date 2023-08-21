using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation
{
    internal class ConfirmUserValidator : AbstractValidator<ConfirmUser>
    {
        public ConfirmUserValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.VerificationCode).VerificationCode();
            RuleFor(x => x.Token).UserToken();
        }
    }
}
