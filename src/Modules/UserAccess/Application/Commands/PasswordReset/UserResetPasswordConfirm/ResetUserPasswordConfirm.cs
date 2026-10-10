using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm
{
    public record ResetUserPasswordConfirm : IPersistable
    {
        public string PhoneNumber { get; init; }
        public string NewPassword { get; init; }
        public string Token { get; init; }

    }
}
