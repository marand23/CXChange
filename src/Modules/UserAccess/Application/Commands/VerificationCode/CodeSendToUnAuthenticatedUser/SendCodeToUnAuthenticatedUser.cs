namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser
{
    public record SendCodeToUnAuthenticatedUser
    {
        public string PhoneNumber { get; set; }
        public string CodeType { get; set; }
        public string Token { get; set; }
    }
}
