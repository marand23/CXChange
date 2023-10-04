namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.CodeSendToUnAuthenticatedUser
{
    public record SendCodeToPhoneNumber
    {
        public string PhoneNumber { get; init; }
        public string CodeType { get; init; }
        public string Token { get; init; }
        public string UserId { get; init; }
    }
}
