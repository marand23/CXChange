namespace CXChange.Modules.Payment.Domain.User.ValueObjects
{
    public record UserPhoneNumber
    {
        public UserPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public string? PhoneNumber { get; init; }
    }
}
