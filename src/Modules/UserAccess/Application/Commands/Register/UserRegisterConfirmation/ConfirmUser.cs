using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Register.UserRegisterConfirmation
{
    public record ConfirmUser : IPersistable, IValidatable, ICommandBase
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }

    }
}
