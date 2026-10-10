using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.User.DomainEvents;
using CXChange.Modules.UserAccess.Domain.User.Specifications;

namespace CXChange.Modules.UserAccess.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        private User(string phoneNumber, string password, bool twoFactorEnabled, string username, Guid id)
        {

            PhoneNumber = phoneNumber;
            Password = password;
            UserName = phoneNumber;
            TwoFactorEnabled = twoFactorEnabled;
            SecurityStamp = Guid.NewGuid().ToString();
            UserName = username;
            Id = id;
            SetDefaultTwoFactor();
            AddDomainEvent(new UserRegisteredDomainEvent(this));

        }


        private User(SnapShot snapShot)
        {
            Email = snapShot.Email;
            SecurityStamp = snapShot.SecurityStamp;
            UserName = snapShot.UserName;
            PhoneNumber = snapShot.PhoneNumber;
            TwoFactorEnabled = snapShot.TwoFactorEnabled;
            ActiveTwoFactor = snapShot.ActiveTwoFactor;
            PhoneNumberConfirmed = snapShot.PhoneNumberConfirmed;
            Id = snapShot.Id;
            IsRestricted = snapShot.IsRestricted;
            EmailConfirmed = snapShot.EmailConfirmed;
            IsAuthenticatorActive = snapShot.IsAuthenticatedActive;


        }

        private string Email { get; set; }
        private string SecurityStamp { get; set; }
        private string PhoneNumber { get; set; }
        private string Password { get; set; }
        private bool PhoneNumberConfirmed { get; set; }
        private bool TwoFactorEnabled { get; set; }
        private TwoFactorEnum ActiveTwoFactor { get; set; }
        private bool IsAuthenticatorActive { get; set; }
        private string UserName { get; set; }
        private bool IsRestricted { get; set; }

        private bool EmailConfirmed { get; set; }
        private bool Ban { get; set; }
        private int AccessFailedCount { get; set; }
        private DateTime BanEndDateTime { get; set; }


        public void CheckRegisterRequirements()
        {
            CheckRule(new UserShoulNotBeBanned(Ban));
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            CheckRule(new UsersPhoneNumberShouldNotBeConfirmed(PhoneNumberConfirmed));
        }
        public static User RegisterNewUser(string phoneNumber, string password, bool twoFactorEnabled, string username, Guid id)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(PhoneNumberConfirmed));
            ArgumentException.ThrowIfNullOrEmpty(nameof(Password));

            return new User(phoneNumber, password, twoFactorEnabled, username, id);




        }

        public SnapShot GetSnapShot()
        {

            return new SnapShot
            {
                Id = Id,
                TwoFactorEnabled = TwoFactorEnabled,
                ActiveTwoFactor = ActiveTwoFactor,
                PhoneNumber = PhoneNumber,
                Password = Password,
                SecurityStamp = SecurityStamp,
                UserName = UserName,
                Email = Email,
                PhoneNumberConfirmed = PhoneNumberConfirmed,
                IsAuthenticatedActive = IsAuthenticatorActive
            };
        }
        public static User CreateFrom(SnapShot snapShot)
        {

            return new User(snapShot);
        }



        public void ConfirmUserRegister()
        {
            CheckRegisterRequirements();
            PhoneNumberConfirmed = true;
            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this));
        }
        public void LoginUser()
        {
            EnforceUserOperationRequirements();
            this.CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            this.AddDomainEvent(new UserLoginCheckedDomainEvent(this));

        }
        public void ConfirmUserLogin()
        {
            EnforceUserOperationRequirements();
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));

            AddDomainEvent(new UserLoginCheckedDomainEvent(this));
        }
        public void CheckUserPasswordReset()
        {
            EnforceUserOperationRequirements();
            CheckRule(new UserShouldBeConfirmed(PhoneNumberConfirmed));
            CheckRule(new UserShoudNotBeRestricted(IsRestricted));
            AddDomainEvent(new UserPasswordResetDomainEvent(this));
        }
        public void ActivateEmail(string email)
        {
            EnforceUserOperationRequirements();
            CheckRule(new EmailShouldNotBeConfirmed(EmailConfirmed, "email should not be confirmed when activating email"));
            this.Email = email;
            this.EmailConfirmed = true;
            SetDefaultTwoFactor();
            AddDomainEvent(new EmailActivatedDomainEvent(this));
        }
        public void UpdateSecurityStamp()
        {
            SecurityStamp = Guid.NewGuid().ToString();
        }
        public void DisableEmail()
        {
            EnforceUserOperationRequirements();
            CheckRule(new EmailShouldBeConfirmed(EmailConfirmed, "email should be confirmed when disable email"));
            this.EmailConfirmed = false;
            this.Email = null;
            SetDefaultTwoFactor();
            AddDomainEvent(new UserEmailDeactivatedDomainEvent(this));
        }
        public void ActivateAuthenticator()
        {
            EnforceUserOperationRequirements();
            IsAuthenticatorActive = true;
            SetDefaultTwoFactor();
            AddDomainEvent(new AuthenticatorActivatedDomainEvent(this));

        }
        private void SetDefaultTwoFactor()
        {
            if (IsAuthenticatorActive)
            {
                ActiveTwoFactor = TwoFactorEnum.AUTHENTICATOR;
                return;
            }
            if (EmailConfirmed)
            {
                ActiveTwoFactor = TwoFactorEnum.EMAIL;
                return;
            }
            ActiveTwoFactor = TwoFactorEnum.SMS;

        }
        public void DeativateAuthenticator()
        {
            EnforceUserOperationRequirements();
            IsAuthenticatorActive = false;
            SetDefaultTwoFactor();
            AddDomainEvent(new AuthenticatorDeactivatedDomainEvent(this));
        }
        public void ChangeEmail(string email)
        {
            EnforceUserOperationRequirements();
            CheckRule(new EmailShouldBeConfirmed(EmailConfirmed, "email should be confirmed on email change"));
            Email = email;
            AddDomainEvent(new EmailChangedDomainEvent(this));

        }
        public void BanUser()
        {
            Ban = true;

        }
        private void EnforceUserOperationRequirements()
        {
            CheckRule(new UserShouldNotBeBanned(Ban));
            CheckRule(new UserShouldBeConfirmed(PhoneNumberConfirmed));

        }
    }
}
