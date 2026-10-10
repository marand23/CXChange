namespace CXChange.Modules.UserAccess.Application.Commands.PasswordReset.UserResetPasswordVerifCode
{
    public record ResetPasswordCodeVerified
    {
        public string Token { get; init; }
    }

}
