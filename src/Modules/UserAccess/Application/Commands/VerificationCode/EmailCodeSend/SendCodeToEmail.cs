namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend
{
    public record SendCodeToEmail
    {
        public string Email { get; init; }
        public string CodeType { get; init; }
        public string Token { get; init; }
        public string UserId { get; init; }
    }
}
