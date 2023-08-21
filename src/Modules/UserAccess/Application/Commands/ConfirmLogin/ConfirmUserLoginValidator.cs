using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin
{
    internal class ConfirmUserLoginValidator : AbstractValidator<ConfirmUserLogin>
    {
        public ConfirmUserLoginValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.VerificationCode).VerificationCode();
            RuleFor(x => x.Token).UserToken();
        }
    }
}
