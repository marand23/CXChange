using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordReset
{
    public record CheckUserPasswordReset : IPersistable, IValidatable
    {
        public string PhoneNumber { get; set; }

    }
}
