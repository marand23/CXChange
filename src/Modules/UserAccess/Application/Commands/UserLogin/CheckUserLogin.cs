using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserLogin
{
    public record CheckUserLogin : IValidatable, IPersistable, ICommandBase
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
