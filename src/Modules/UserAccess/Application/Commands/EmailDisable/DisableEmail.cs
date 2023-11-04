using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailDisable
{
    public record DisableEmail : IPersistable, IValidatable, ICommandBase
    {
        public Guid UserId { get; init; }
        public string Token { get; init; }

    }
}
