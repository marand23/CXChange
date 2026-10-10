using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{
    internal interface IExchangeUserManager
    {
        bool SupportsUserLockout { get; }
        Task<AppUser> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        Task<bool> VerifyUserTokenAsync(AppUser user, string tokenProvider, string purpose, string token);
        Task<string> GenerateUserTokenAsync(AppUser user, string tokenProvider, string purpose);
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<IdentityResult> AccessFailedAsync(AppUser user);
        Task<bool> IsLockedOutAsync(AppUser user);
        Task<bool> GetTwoFactorEnabledAsync(AppUser user);
        Task<IdentityResult> UpdateAsync(AppUser user);
        Task<IdentityResult> UpdateSecurityStampAsync(AppUser user);
        Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword);
        Task<AppUser> FindByIdAsync(Guid userUd);

        Task<AppUser> GetUserByPhoneNumber(string PhoneNumber);
        Task<byte[]> GetUserSecurityToken(AppUser user);
        Task<IdentityResult> DeleteAsync(AppUser user);
        Task ConfirmUserPhoneNumber(string userId);
        Task<IdentityResult> ChangePassword(AppUser user, string oldPassword, string newPassword);
        Task<bool> CheckPassword(AppUser user, string password);
        Task<IdentityResult> ResetAuthenticatorKey(AppUser user);
        Task<string> GetAuthenticatorKey(AppUser user);
        Task<AppUser> GetUserByEmail(string email);
        Task<IdentityResult> SetAuthenticationTokenAsync(AppUser user, string loginProvider, string tokenName, string? tokenValue);
        Task<int> MaxFailedAccessAttemps();


    }
}
