namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate
{
    public record AuthenticatorCodeValidated
    {
        public string Token { get; set; }
    }
}
