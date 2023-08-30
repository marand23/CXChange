using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.Register.UserRegistration
{
    public record RegisterUser : ICommandBase, IPersistable, IValidatable
    {
        public string PhoneNumber { get; init; }
        public string Password { get; init; }
    }
}
