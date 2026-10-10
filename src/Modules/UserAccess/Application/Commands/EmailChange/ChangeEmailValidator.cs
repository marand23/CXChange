using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailChange
{
    internal class ChangeEmailValidator : AbstractValidator<ChangeEmail>
    {
        public ChangeEmailValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.UserId).UserId();
            RuleFor(x => x.Token).UserToken();
            RuleFor(x => x.Code).VerificationCode();
        }
    }
}
