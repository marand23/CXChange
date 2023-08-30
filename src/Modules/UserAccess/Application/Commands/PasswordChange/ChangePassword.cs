namespace CXChange.Modules.UserAccess.Application.Commands.PasswordChange
{
    public record ChangePassword
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
