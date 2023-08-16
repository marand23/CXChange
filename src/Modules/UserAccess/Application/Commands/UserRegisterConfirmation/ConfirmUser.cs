using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation
{
    public record ConfirmUser : IPersistable
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
    }
}
