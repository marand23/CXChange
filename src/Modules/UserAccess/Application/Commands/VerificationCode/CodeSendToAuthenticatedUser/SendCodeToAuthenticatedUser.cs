namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToAuthenticatedUser
{
    public record SendCodeToAuthenticatedUser
    {
        public string UserId { get; set; }
        public string CodeType { get; set; }
        public string Token { get; set; }

    }
}
