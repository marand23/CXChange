using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailActivate
{
    public class ActivateEmailValidator : AbstractValidator<ActivateEmail>
    {
        public ActivateEmailValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Token).UserToken();

        }
    }
}
