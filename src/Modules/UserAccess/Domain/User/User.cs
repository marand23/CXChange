using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Domain.User.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User
{
    public class User : Entity, IAggregateRoot
    {
        public User(string phoneNumber, string password, string defaultTwoFactor, bool twoFactorEnabled)
        {

            PhoneNumber = phoneNumber;
            Password = password;
            this.DefaultTwoFactor = defaultTwoFactor;
            TwoFactorEnabled = twoFactorEnabled;

        }
        public User() { }
        private User(string phoneNumber, bool twoFactorEnabled, string defaultTwoFactor, string DefaultTwoFactor, string password)
        {
            PhoneNumber = phoneNumber;

            TwoFactorEnabled = twoFactorEnabled;
            DefaultTwoFactor = defaultTwoFactor;
        }

        private string Email { get; set; }
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        private string PhoneNumber { get; set; }
        private string Password { get; set; }
        private bool PhoneNumberConfirmed { get; set; }
        private bool TwoFactorEnabled { get; set; }
        private string DefaultTwoFactor { get; set; }
        private string UserName { get; set; }
        private bool IsRestricted { get; set; }


        public void CheckRegisterPossiblity()
        {
            this.CheckRule(new UsersPhoneNumberShouldNotBeConfirmedOnRegistration(PhoneNumberConfirmed));
            this.CheckRule(new UserShoudNotBeRestrictedOnRegistration(IsRestricted));



        }
        public User RegisterNewUser(string phoneNumber, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(PhoneNumberConfirmed));
            ArgumentException.ThrowIfNullOrEmpty(nameof(Password));


            var user = new User(phoneNumber, password, "SMS", true);
            this.AddDomainEvent(new UserRegisteredDomainEvent(user));
            return user;
        }
        public SnapShot GetSnapShot()
        {

            return new SnapShot
            {
                TwoFactorEnabled = TwoFactorEnabled,
                DefaultTwoFactor = DefaultTwoFactor,
                PhoneNumber = PhoneNumber,
                Password = Password,
                SecurityStamp = SecurityStamp,

            };
        }


        private void ConfirmPhoneNumber()
        {
            PhoneNumberConfirmed = true;
        }




    }
}
