using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.AuthenticatorDeactivate
{
    public class DeactivateAuthenticatorValidator : AbstractValidator<DeactivateAuthenticator>
    {
        public DeactivateAuthenticatorValidator()
        {
            RuleFor(x => x.Token).UserToken();
        }
    }
}
