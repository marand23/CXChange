using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailDisable
{
    public class DisableEmailValidator : AbstractValidator<DisableEmail>
    {
        public DisableEmailValidator()
        {
            RuleFor(x => x.Token).UserToken();
        }
    }
}
