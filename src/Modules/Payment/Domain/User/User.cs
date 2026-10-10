using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.Payment.Domain.User.Specifications;
using CXChange.Modules.Payment.Domain.User.ValueObjects;
namespace CXChange.Modules.Payment.Domain.User
{
    public class User : Entity<Guid>, IAggregateRoot
    {
        private User() { }

        public UserPhoneNumber PhoneNumber { get; private set; }
        public bool CanDeposit { get; private set; }
        public bool CanWithdraw { get; private set; }

        private User(string phoneNumber, Guid userId)
        {
            PhoneNumber = new UserPhoneNumber(phoneNumber);
            Id = userId;
        }



        public static User CreateUser(string phoneNumber, Guid userId)
        {
            return new User(phoneNumber, userId);
        }
        public void CheckUserDepositRequirements()
        {
            CheckRule(new UserShouldHaveDepositPermissionsOnDeposit(CanDeposit));
        }
        public void CheckUserWithdrawRequirements()
        {
            CheckRule(new UserShouldHaveWithdrawPermissionOnWithdraw(CanWithdraw));
        }


    }
}
