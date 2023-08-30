namespace CXChange.Modules.UserAccess.Application.Commands.UserTwoFactorTypeChangeConfirm
{
    public record ConfirmUserTwoFactorTypeChange
    {
        public string UserId { get; set; }
        public string NewTwoFactorType { get; set; }
        public string Token { get; set; }
    }
}
