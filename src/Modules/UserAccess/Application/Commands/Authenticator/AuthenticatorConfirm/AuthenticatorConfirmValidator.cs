using FluentValidation;

namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    public class AuthenticatorConfirmValidator : AbstractValidator<ConfirmAuthenticator>
    {
        public AuthenticatorConfirmValidator()
        {
            RuleFor(x => x.AuthToken).UserToken();
            RuleFor(x => x.CodeToken).UserToken();

        }
    }
}
