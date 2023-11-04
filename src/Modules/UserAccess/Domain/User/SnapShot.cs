namespace CXChange.Modules.UserAccess.Domain.User
{
    public class SnapShot
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }

        public string UserName { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool IsRestricted { get; set; }
        public TwoFactorEnum ActiveTwoFactor { get; set; }
        public bool IsAuthenticatedActive { get; set; }
    }
}
