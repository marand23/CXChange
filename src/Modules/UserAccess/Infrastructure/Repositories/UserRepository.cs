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
                SecurityStamp = snapShot.SecurityStamp,

            };


            var createResult = await _exchangeUserManager.CreateAsync(appUser, snapShot.Password);
            appUser.SecurityStamp = snapShot.SecurityStamp;
            if (!createResult.Succeeded) { throw new InvalidOperationException(createResult.Errors.FirstOrDefault().Description); }






        }

        public async Task Update(User user)
        {
            var snapShot = user.GetSnapShot();
            AppUser appUser = await _exchangeUserManager.FindByIdAsync(snapShot.Id);
            Map(appUser, user.GetSnapShot());



        }

        public void Map(AppUser appuser, SnapShot snapShot)
        {

            appuser.Email = snapShot.Email;
            appuser.UserName = snapShot.UserName;
            appuser.PhoneNumber = snapShot.PhoneNumber;
            appuser.PhoneNumberConfirmed = snapShot.PhoneNumberConfirmed;
            appuser.DefaultTwoFactor = snapShot.DefaultTwoFactor;
            appuser.SecurityStamp = snapShot.SecurityStamp;
            appuser.EmailConfirmed = snapShot.EmailConfirmed;
            appuser.TwoFactorEnabled = snapShot.TwoFactorEnabled;


        }

        public async Task<User> GetUserById(string id)
        {

            AppUser user = await _exchangeUserManager.FindByIdAsync(id);





            if (user != null)
            {

                SnapShot snapShot = new SnapShot();
                snapShot.PhoneNumber = user.PhoneNumber;
                snapShot.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                snapShot.DefaultTwoFactor = user.DefaultTwoFactor;
                snapShot.SecurityStamp = user.SecurityStamp;
                snapShot.Email = user.Email;
                snapShot.Id = user.Id;
                snapShot.UserName = user.UserName;
                snapShot.ConcurrencyStamp = user.ConcurrencyStamp;

                return User.CreateFrom(snapShot);
            }
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
        public async Task DeleteUser(string userId)
        {
            AppUser user = await _exchangeUserManager.FindByIdAsync(userId);


            await _exchangeUserManager.DeleteAsync(user);
        }
        public async Task CheckUserPassword(string id, string Password)
        {
            var user = await _exchangeUserManager.FindByIdAsync(id);
            await _exchangeUserManager.CheckPasswordAsync(user, Password);
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
            var appUser = await _exchangeUserManager.FindByIdAsync(snapShot.Id);
            string token = await _exchangeUserManager.GenerateUserTokenAsync(appUser, tokenProvider, purpose);
            return token;

        }

        public async Task<bool> ValidateUserToken(User user, string token, string tokenProvider, string purpose)
        {
            SnapShot snapShot = user.GetSnapShot();
            var appUser = await _exchangeUserManager.FindByIdAsync(snapShot.Id);
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
            var appUser = await _exchangeUserManager.FindByIdAsync(snapshot.Id);
            var token = await _exchangeUserManager.GeneratePasswordResetTokenAsync(appUser);
            return token;
        }

        public async Task ResetPassword(User user, string token, string newPassword)
        {
            var snapshot = user.GetSnapShot();
            var appUser = await _exchangeUserManager.FindByIdAsync(snapshot.Id);
            var result = await _exchangeUserManager.ResetPasswordAsync(appUser, token, newPassword);

        }

        public async Task ChangePassword(User user, string oldPassword, string newPassword)
        {
            var appUser = await _exchangeUserManager.FindByIdAsync(user.GetSnapShot().Id);
            await _exchangeUserManager.ChangePassword(appUser, oldPassword, newPassword);
        }

        public async Task<bool> CheckPassword(User user, string password)
        {
            var appUser = await _exchangeUserManager.FindByIdAsync(user.GetSnapShot().Id);
            return await _exchangeUserManager.CheckPassword(appUser, password);
        }

        public async Task ResetAuthenticatorKey(User user)
        {
            var appUser = await _exchangeUserManager.FindByIdAsync(user.GetSnapShot().Id);
            await _exchangeUserManager.ResetAuthenticatorKey(appUser);
        }

        public async Task<string> GetAuthenticatorKey(User user)
        {
            var appUser = await _exchangeUserManager.FindByIdAsync(user.GetSnapShot().Id);
            return await _exchangeUserManager.GetAuthenticatorKey(appUser);
        }
    }
}
