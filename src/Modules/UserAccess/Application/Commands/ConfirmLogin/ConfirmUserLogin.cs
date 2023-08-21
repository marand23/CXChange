using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.ConfirmLogin
{
    public record ConfirmUserLogin : ICommandBase, IPersistable, IValidatable
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string VerificationCode { get; set; }
    }
}
