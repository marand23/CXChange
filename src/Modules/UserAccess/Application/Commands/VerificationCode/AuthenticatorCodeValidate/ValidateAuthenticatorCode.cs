namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.AuthenticatorCodeValidate
{
    public record ValidateAuthenticatorCode
    {
        public string UserId { get; init; }
        public string Code { get; init; }
        public string CodeType { get; init; }
    }
}
