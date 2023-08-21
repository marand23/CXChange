using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserRegisterConfirmation
{
    public record ConfirmUser : IPersistable, IValidatable, ICommandBase
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Token { get; set; }
    }
}
