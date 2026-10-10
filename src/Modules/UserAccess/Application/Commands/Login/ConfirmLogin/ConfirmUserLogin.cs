using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Login.ConfirmLogin
{
    public record ConfirmUserLogin : ICommandBase, IPersistable, IValidatable
    {
        public string PhoneNumber { get; init; }
        public string Token { get; init; }
        public string Code { get; init; }
    }
}
