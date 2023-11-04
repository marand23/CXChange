using CXChange.BuildingBlocks.Application.MessageInterfaces;

namespace CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm
{
    public record ConfirmUserTwoFactorTypeChange : IPersistable
    {
        public Guid UserId { get; init; }
        public int NewTwoFactorType { get; init; }
        public string Token { get; init; }
    }
}
