using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.VerificationCode.EmailCodeSend
{
    public record SendCodeToEmail : INeedsAuthentication
    {
        public string Email { get; init; }
        public string CodeType { get; init; }
        public string Token { get; init; }
        public Guid UserId { get; set; }
    }
}
