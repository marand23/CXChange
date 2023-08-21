namespace CXChange.Modules.UserAccess.Application.Commands.UserRegistration
{
    public record UserRegistered
    {
        public string PhoneNumber { get; set; }
        public TwoFactorType TwoFactorType { get; set; }
        public string Token { get; set; }
    }
    public enum TwoFactorType
    {
        SMS,
        Email,
        Authenticator
    }
}
