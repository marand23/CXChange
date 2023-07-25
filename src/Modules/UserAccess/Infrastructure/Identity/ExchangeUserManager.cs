using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{
    internal class ExchangeUserManager : IExchangeUserManager
    {
        private readonly UserManager<AppUser> userManager;
        //private readonly ITOTP _totp;
        //private readonly IAuthenticator _authenticator;
        //CustomIDataProtection<T> customIDataProtection;
        public ExchangeUserManager(UserManager<AppUser> userManager/*, ITOTP totp, IAuthenticator authenticator, CustomIDataProtection<T> customIDataProtection*/)
        {
            this.userManager = userManager;
            //this._totp = totp;
            //this._authenticator = authenticator;
            //this.customIDataProtection = customIDataProtection;


        }
        //public async Task<AppUser> GetUserByToken(string token)
        //{
        //    //var id = customIDataProtection.GetUserIdByToken(token);

        //    //return await userManager.FindByIdAsync(id);

        //}
        //public async Task<AppUser> GetUserByPassToken(string token)
        //{

        //    var id = customIDataProtection.GetUserIdByPassToken(token);
        //    return await userManager.FindByIdAsync(id);
        //}

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
        //public async Task<string> GenerateTotpCode(AppUser user, TimeSpan timeSpan, string modefier, int numberOfDigits)
        //{
        //    var token = await userManager.CreateSecurityTokenAsync(user);

        //    var code = _totp.GenerateCode(token, timeSpan, modefier.ToString(), numberOfDigits);
        //    return code.ToString("D" + numberOfDigits.ToString(), CultureInfo.InvariantCulture);
        //}
        //public async Task<bool> ValidateTotpCode(AppUser user, TimeSpan timeSpan, int code, string modefier, int numberOfDigits, string type)
        //{
        //    if (type == "Sms" || type == "Email")
        //    {
        //        var token = await userManager.CreateSecurityTokenAsync(user);
        //        return _totp.ValidateCode(token, code, timeSpan, modefier.ToString(), numberOfDigits);

        //    }
        //    else
        //    {

        //        var secret = await userManager.GetAuthenticatorKeyAsync(user);
        //        return _authenticator.ValidateAuthenticatorCode(secret, code.ToString(), TimeSpan.FromSeconds(40));
        //    }

        //}
       
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
        public async Task<AppUser> GetUserById(string id)
        {
           return await userManager.FindByIdAsync(id);
         

        }
        public async Task<AppUser> GetUserByPhoneNumber(string PhoneNumber)
        {
            return await userManager.Users.FirstOrDefaultAsync(u=>u.PhoneNumber == PhoneNumber);
        }
       public async Task<byte[]> GetUserSecurityToken(AppUser user)
        {
            return await userManager.CreateSecurityTokenAsync(user);
        }


    }
}
