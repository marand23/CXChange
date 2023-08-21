using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordResetConfirmation
{
    public record class ConfirmUserPasswordReset : ICommandBase, IValidatable, IPersistable
    {
        public string PhoneNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
