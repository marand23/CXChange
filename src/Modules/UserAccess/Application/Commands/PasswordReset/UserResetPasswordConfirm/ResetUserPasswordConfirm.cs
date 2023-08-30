namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordConfirm
{
    public record ResetUserPasswordConfirm
    {
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }

    }
}
