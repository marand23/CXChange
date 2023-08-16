using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin
{
    public record ConfirmUserLogin : IPersistable
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string VerificationCode { get; set; }
    }
}
