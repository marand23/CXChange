using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin
{
    public class ConfirmUserLoginValidator : AbstractValidator<ConfirmUserLogin>
    {
        public ConfirmUserLoginValidator()
        {
            RuleFor(x => x.PhoneNumber).PhoneNumber();
            RuleFor(x => x.Code).VerificationCode();
            RuleFor(x => x.Token).UserToken();
        }
    }
}
