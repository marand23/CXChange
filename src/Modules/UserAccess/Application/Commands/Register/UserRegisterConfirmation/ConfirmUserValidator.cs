using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation
{
    internal class ConfirmUserValidator : AbstractValidator<ConfirmUser>
    {
        public ConfirmUserValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.VerificationCode).VerificationCode();

        }
    }
}
