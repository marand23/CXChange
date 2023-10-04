using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{

    internal class ExchangeUserManager : IExchangeUserManager
    {
        private readonly UserManager<AppUser> userManager;
        public ExchangeUserManager(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }


        public bool SupportsUserLockout
        {
            get { return userManager.SupportsUserLockout; }
        }
        public async Task<AppUser> FindByNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);



        }
        public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        {

            return await userManager.CreateAsync(user, password);

        }
        public async Task<IdentityResult> DeleteAsync(AppUser user)
        {
            return await userManager.DeleteAsync(user);
        }

        public async Task<bool> VerifyUserTokenAsync(AppUser user, string tokenProvider, string purpose, string token)
        {
            return await userManager.VerifyUserTokenAsync(user, tokenProvider, purpose, token);
        }
        public async Task<string> GenerateUserTokenAsync(AppUser user, string tokenProvider, string purpose)
        {
            return await userManager.GenerateUserTokenAsync(user, tokenProvider, purpose);


        }
        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);

        }
        public async Task<IdentityResult> AccessFailedAsync(AppUser user)
        {
            return await userManager.AccessFailedAsync(user);
        }
        public async Task<bool> IsLockedOutAsync(AppUser user)
        {

            return await userManager.IsLockedOutAsync(user);
        }
        public async Task<bool> GetTwoFactorEnabledAsync(AppUser user)
        {
            return await userManager.GetTwoFactorEnabledAsync(user);
        }
        public async Task<IdentityResult> UpdateAsync(AppUser user)
        {
            return await userManager.UpdateAsync(user);

        }

        public Task<IdentityResult> UpdateSecurityStampAsync(AppUser user)
        {

            return userManager.UpdateSecurityStampAsync(user);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);

        }
        public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        {
            return await userManager.ResetPasswordAsync(user, token, newPassword);
        }
        public async Task<AppUser> FindByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);


        }
        public async Task<AppUser> GetUserByPhoneNumber(string PhoneNumber)
        {
            return await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == PhoneNumber);


        }
        public async Task<byte[]> GetUserSecurityToken(AppUser user)
        {
            return await userManager.CreateSecurityTokenAsync(user);
        }
        public async Task ConfirmUserPhoneNumber(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            user.PhoneNumberConfirmed = true;
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, string oldPassword, string newPassword)
        {
            var res = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return res;
        }

        public async Task<bool> CheckPassword(AppUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }
        public async Task<IdentityResult> ResetAuthenticatorKey(AppUser user)
        {
            return await userManager.ResetAuthenticatorKeyAsync(user);
        }
        public async Task<string> GetAuthenticatorKey(AppUser user)
        {
            return await userManager.GetAuthenticatorKeyAsync(user);
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> SetAuthenticationTokenAsync(AppUser user, string loginProvider, string tokenName, string? tokenValue)
        {
            return await userManager.SetAuthenticationTokenAsync(user, loginProvider, tokenName, tokenValue);
        }

        public Task<int> MaxFailedAccessAttemps()
        {
            return Task.FromResult(userManager.Options.Lockout.MaxFailedAccessAttempts);
        }

    }
}
