using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.EmailActivate
{
    public record ActivateEmail : IPersistable, IValidatable, ICommandBase
    {
        public string UserId { get; init; }
        public string Email { get; init; }
        public string Token { get; init; }
        public string Code { get; init; }
    }
}
