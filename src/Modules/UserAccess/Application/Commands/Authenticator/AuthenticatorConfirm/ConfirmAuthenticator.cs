namespace CXChange.Modules.UserAccess.Application.Commands.Authenticator.AuthenticatorConfirm
{
    public record ConfirmAuthenticator
    {
        public string UserId { get; set; }
        public string AuthToken { get; set; }
        public string CodeToken { get; set; }
    }
}
