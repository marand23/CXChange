using CXChange.Modules.UserAccess.Domain.User;

namespace CXChange.Modules.UserAccess.Application.Queries.UserGet
{
    public record Result
    {
        public string PhoneNumber { get; set; }
        public TwoFactorEnum DefaultTwoFactor { get; set; }
        public string IsPhoneConfirmed { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsTotpConfirmed { get; set; }

    }
}
