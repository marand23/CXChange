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
            SecurityStamp = Guid.NewGuid().ToString();
        }

        private User(string phoneNumber, bool twoFactorEnabled, string defaultTwoFactor, string password)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(phoneNumber, nameof(phoneNumber));
            ArgumentNullException.ThrowIfNullOrEmpty(password, nameof(password));
            ArgumentNullException.ThrowIfNullOrEmpty(defaultTwoFactor, nameof(defaultTwoFactor));

            PhoneNumber = phoneNumber;

            TwoFactorEnabled = twoFactorEnabled;
            DefaultTwoFactor = defaultTwoFactor;
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = phoneNumber;
        }
        private User(SnapShot snapShot)
        {
            Email = snapShot.Email;
            SecurityStamp = snapShot.SecurityStamp;
            UserName = snapShot.UserName;
            PhoneNumber = snapShot.PhoneNumber;
            TwoFactorEnabled = snapShot.TwoFactorEnabled;
            DefaultTwoFactor = snapShot.DefaultTwoFactor;
            PhoneNumberConfirmed = snapShot.PhoneNumberConfirmed;
            Id = snapShot.Id;

        }
        private string Id { get; set; }
        private string Email { get; set; }
        private string SecurityStamp { get; set; }
        private string PhoneNumber { get; set; }
        private string Password { get; set; }
        private bool PhoneNumberConfirmed { get; set; }
        private bool TwoFactorEnabled { get; set; }
        private string DefaultTwoFactor { get; set; }
        private string UserName { get; set; }
        private bool IsRestricted { get; set; }


        public void CheckRegisterPossiblity()
        {
            this.CheckRule(new UserShoudNotBeRestrictedOnRegistration(IsRestricted));
            this.CheckRule(new UsersPhoneNumberShouldNotBeConfirmedOnRegistration(PhoneNumberConfirmed));




        }
        public void RegisterNewUser()
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(PhoneNumberConfirmed));
            ArgumentException.ThrowIfNullOrEmpty(nameof(Password));


            this.AddDomainEvent(new UserRegisteredDomainEvent(this));

        }

        public SnapShot GetSnapShot()
        {

            return new SnapShot
            {
                Id = Id,
                TwoFactorEnabled = TwoFactorEnabled,
                DefaultTwoFactor = DefaultTwoFactor,
                PhoneNumber = PhoneNumber,
                Password = Password,
                SecurityStamp = SecurityStamp,
                UserName = UserName,
                Email = Email,
                PhoneNumberConfirmed = PhoneNumberConfirmed,

            };
        }
        public static User CreateFrom(SnapShot snapShot)
        {
            return new User(snapShot);
        }


        public void ConfirmPhoneNumber()
        {
            this.CheckRule(new UserShoudNotBeRestrictedOnRegistration(IsRestricted));
            this.CheckRule(new UsersPhoneNumberShouldNotBeConfirmedOnRegistration(PhoneNumberConfirmed));
            PhoneNumberConfirmed = true;
            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this));
        }
        public void LoginUser()
        {
            this.CheckRule(new UserShoudNotBeRestrictedOnRegistration(IsRestricted));
            this.AddDomainEvent(new UserLoginCheckedDomainEvent(this));

        }
        public void ConfirmUserLogin()
        {
            this.CheckRule(new UserShoudNotBeRestrictedOnRegistration(IsRestricted));

        }




    }
}
