using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Domain.User.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User
{
    public class User : Entity, IAggregateRoot
    {
        private User(string phoneNumber, string password, string defaultTwoFactor, bool twoFactorEnabled)
        {

            PhoneNumber = phoneNumber;
            Password = password;
            this.DefaultTwoFactor = defaultTwoFactor;
            TwoFactorEnabled = twoFactorEnabled;
            SecurityStamp = Guid.NewGuid().ToString();
            Id = Guid.NewGuid().ToString();
            AddDomainEvent(new UserRegisteredDomainEvent(this));

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
            IsRestricted = snapShot.IsRestricted;
            EmailConfirmed = snapShot.EmailConfirmed;


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
        private string ConcurrencyStamp { get; set; }
        private bool EmailConfirmed { get; set; }

        public void CheckRegisterPossiblity()
        {
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            CheckRule(new UsersPhoneNumberShouldNotBeConfirmed(PhoneNumberConfirmed));
        }
        public static User RegisterNewUser(string phoneNumber, string password, string defaultTwoFactor, bool twoFactorEnabled)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(PhoneNumberConfirmed));
            ArgumentException.ThrowIfNullOrEmpty(nameof(Password));
            return new User(phoneNumber, password, defaultTwoFactor, twoFactorEnabled);




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


        public void ConfirmUser()
        {
            this.CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            this.CheckRule(new UsersPhoneNumberShouldNotBeConfirmed(PhoneNumberConfirmed));
            PhoneNumberConfirmed = true;
            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this));
        }
        public void LoginUser()
        {
            this.CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            this.AddDomainEvent(new UserLoginCheckedDomainEvent(this));

        }
        public void ConfirmUserLogin()
        {
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));

            AddDomainEvent(new UserLoginCheckedDomainEvent(this));
        }
        public void CheckUserPasswordReset()
        {
            CheckRule(new UserShouldBeConfirmed(PhoneNumberConfirmed));
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            AddDomainEvent(new UserPasswordResetCheckedDomainEvent(this));
        }
        public void ChangeTwoFactorType(string newTwoFactorType)
        {
            CheckRule(new OldTwoFactorTypeAndNewTwoFactorTypeShouldNotBeTheSame(DefaultTwoFactor, newTwoFactorType));
            DefaultTwoFactor = newTwoFactorType;
            AddDomainEvent(new UserTwoFactorTypeChangedDomainEvent(this));
        }
        public void ActivateEmail(string email)
        {
            CheckRule(new EmailShouldNotBeActiveOnEmailActivation(EmailConfirmed));
            this.Email = email;
            this.EmailConfirmed = true;
            AddDomainEvent(new EmailActivatedDomainEvent(this));
        }







    }
}
