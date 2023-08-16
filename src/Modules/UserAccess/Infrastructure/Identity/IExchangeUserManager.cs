using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{
    internal interface IExchangeUserManager
    {
        bool SupportsUserLockout { get; }
        Task<AppUser> FindByNameAsync(string userName);
        Task<IdentityResult> CreateAsync(AppUser user, string password);
        //Task<string> GenerateTotpCode(AppUser user, TimeSpan timeSpan, string modefier, int numberOfDigits);
        //Task<bool> ValidateTotpCode(AppUser user, TimeSpan timeSpan, int code, string modefier, int numberOfDigits, string type);
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
        //Task<IdentityUser> GetUserByToken(string token);
        //Task<IdentityUser> GetUserByPassToken(string token);
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByPhoneNumber(string PhoneNumber);
        Task<byte[]> GetUserSecurityToken(AppUser user);
        Task<IdentityResult> DeleteAsync(AppUser user);
        Task ConfirmUserPhoneNumber(string PhoneNumber);

    }
}
