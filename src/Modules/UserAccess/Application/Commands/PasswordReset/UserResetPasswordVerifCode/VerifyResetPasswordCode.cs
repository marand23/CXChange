using CXChange.BuildingBlocks.Application.Command;
using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode
{
    public record VerifyResetPasswordCode : ICommandBase, IPersistable, IValidatable
    {
        public string PhoneNumber { get; init; }
        public string VerificationCode { get; init; }

    }
}
