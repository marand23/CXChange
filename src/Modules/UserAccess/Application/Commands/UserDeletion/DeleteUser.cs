using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserDeletion
{
    public record DeleteUser : ICommandBase, IPersistable
    {
        public string UserId { get; init; }
    }
}
