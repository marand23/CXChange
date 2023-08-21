namespace CXChange.Modules.UserAccess.Application.Commands.UserPasswordReset
{
    public record PasswordResetChecked
    {
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
    }
}
