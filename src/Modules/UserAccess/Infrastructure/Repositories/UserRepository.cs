using CXChange.BuildingBlocks.Domain;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserAccess.Infrastructure.Identity;
using MassTransit.Mediator;

namespace CXChange.Modules.UserAccess.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private IExchangeUserManager _exchangeUserManager;
        private IExchangeSignInManager _exchangeSignInManager;


        public UserRepository(IExchangeUserManager exchangeUserManager, IExchangeSignInManager exchangeSignInManager, IDomainEventDispatcher domainEventDispatcher, IMediator mediator)
        {
            _exchangeSignInManager = exchangeSignInManager;
            _exchangeUserManager = exchangeUserManager;


        }

        public async Task CreateUser(User user)

        {

            var snapShot = user.GetSnapShot();
            var appUser = new AppUser
            {
                UserName = snapShot.PhoneNumber,
                PhoneNumber = snapShot.PhoneNumber,
                DefaultTwoFactor = "SMS",
                Id = snapShot.Id,
                TwoFactorEnabled = snapShot.TwoFactorEnabled,
                SecurityStamp = snapShot.SecurityStamp
            };


            var createResult = await _exchangeUserManager.CreateAsync(appUser, snapShot.Password);
            appUser.SecurityStamp = snapShot.SecurityStamp;
            if (!createResult.Succeeded) { throw new InvalidOperationException(createResult.Errors.FirstOrDefault().Description); }






        }

        public async Task UpdateUser(User user)
        {
            AppUser appUser = new AppUser();
            Map(appUser, user.GetSnapShot());

            var result = await _exchangeUserManager.UpdateAsync(appUser);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault().Description);
            }
        }

        public void Map(AppUser appuser, SnapShot snapShot)
        {

            appuser.Email = snapShot.Email;
            appuser.UserName = snapShot.UserName;
        }

        public async Task<User> GetUserById(string id)
        {
            AppUser user = await _exchangeUserManager.GetUserById(id);

            return null;

        }
        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            AppUser user = await _exchangeUserManager.GetUserByPhoneNumber(phoneNumber);

            if (user != null)
            {

                SnapShot snapShot = new SnapShot();
                snapShot.PhoneNumber = user.PhoneNumber;
                snapShot.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                snapShot.DefaultTwoFactor = user.DefaultTwoFactor;
                snapShot.SecurityStamp = user.SecurityStamp;
                snapShot.Email = user.Email;
                snapShot.Id = user.Id;


                return User.CreateFrom(snapShot);
            }
            return null;




        }
        public async Task DeleteUser(string phoneNumber)
        {
            AppUser user = await _exchangeUserManager.GetUserByPhoneNumber(phoneNumber);


            await _exchangeUserManager.DeleteAsync(user);
        }
        public async Task CheckUserPassword(string PhoneNumber, string Password)
        {

        }
        public Task RemoveTicket(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GetUserSecurityToken(User user)
        {
            return null;
            //    AppUser appUser =await _exchangeUserManager.GetUserById(user.Id.ToString());
            //    return await _exchangeUserManager.GetUserSecurityToken(appUser);
        }

        public async Task ConfirmPhoneNumber(string PhoneNumber)
        {
            await _exchangeUserManager.ConfirmUserPhoneNumber(PhoneNumber);
        }

        public async Task<User> CheckSignIn(string PhoneNumber, string Password)
        {
            var appUser = await _exchangeUserManager.GetUserByPhoneNumber(PhoneNumber);
            if (appUser == null)
            {

            }

            await _exchangeSignInManager.CheckSignIn(appUser, Password, true);
            return User.CreateFrom(new SnapShot
            {
                DefaultTwoFactor = appUser.DefaultTwoFactor,
                Email = appUser.Email,
                SecurityStamp = appUser.SecurityStamp,
                Id = appUser.Id,


            });
        }
        public async Task<string> GenerateUserToken(User user, string tokenProvider, string purpose)
        {

            SnapShot snapShot = user.GetSnapShot();
            var appUser = new AppUser { Id = snapShot.Id, SecurityStamp = snapShot.SecurityStamp };
            string token = await _exchangeUserManager.GenerateUserTokenAsync(appUser, tokenProvider, purpose);
            return token;

        }

        public async Task<bool> ValidateUserToken(User user, string token, string tokenProvider, string purpose)
        {
            SnapShot snapShot = user.GetSnapShot();
            var appUser = await _exchangeUserManager.GetUserByPhoneNumber(snapShot.PhoneNumber);
            bool result = await _exchangeUserManager.VerifyUserTokenAsync(appUser, tokenProvider, purpose, token);
            return result;
        }

        public async Task SignIn(User user)
        {
            SnapShot snapShot = user.GetSnapShot();
            var appUser = await _exchangeUserManager.GetUserByPhoneNumber(snapShot.PhoneNumber);
            var result = await _exchangeSignInManager.ExchangeSignIn(appUser);


        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            var snapshot = user.GetSnapShot();
            var appUser = new AppUser { Id = snapshot.Id, SecurityStamp = snapshot.SecurityStamp };
            var token = await _exchangeUserManager.GeneratePasswordResetTokenAsync(appUser);
            return token;
        }

        public async Task ResetPassword(User user, string token, string newPassword)
        {
            var snapshot = user.GetSnapShot();
            var appUser = new AppUser { Id = snapshot.Id, SecurityStamp = snapshot.SecurityStamp };
            var result = await _exchangeUserManager.ResetPasswordAsync(appUser, token, newPassword);

        }
    }
}
