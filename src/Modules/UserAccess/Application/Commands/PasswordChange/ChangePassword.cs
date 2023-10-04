using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordChange
{
    public record ChangePassword : ICommandBase, IValidatable, IPersistable
    {
        public string UserId { get; init; }
        public string OldPassword { get; init; }
        public string NewPassword { get; init; }
        public string Token { get; init; }
    }
}
