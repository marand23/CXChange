using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{
    internal class ExchangeSignInManager : IExchangeSignInManager
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;



        public ExchangeSignInManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;



        }

        public async Task<SignInResult> CheckSignIn(AppUser user, string password, bool lockoutOnFailure)
        {
            var error = await PreSignInCheck(user);
            if (error != null)
            {
                return error;
            }
            var passwordCheck = await _userManager.CheckPasswordAsync(user, password);
            if (passwordCheck)
            {
                if (_userManager.SupportsUserLockout && lockoutOnFailure)
                {


                    await _userManager.AccessFailedAsync(user);
                    if (await _userManager.IsLockedOutAsync(user))
                    {
                        return await LockedOut(user);
                    }
                }


                return SignInResult.Success;

            }



            return SignInResult.Failed;

        }

        public async Task<SignInResult> ExchangeSignIn(AppUser user)
        {
            var error = await PreSignInCheck(user);
            if (error != null)
            {
                return error;
            }
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            await _signInManager.Context.SignInAsync("CustomScheme", principal);
            return SignInResult.Success;

        }

        public async Task LogOut()
        {
            await _signInManager.Context.SignOutAsync("CustomScheme");

        }

        public Task<SignInResult> ExchangeTwoFactorSignInAsync(AppUser user, string provider, string code, string purpose)
        {
            throw new NotImplementedException();
        }
        protected virtual async Task<SignInResult> PreSignInCheck(AppUser user)
        {
            if (!await _signInManager.CanSignInAsync(user))
            {
                return SignInResult.NotAllowed;
            }
            if (await IsLockedOut(user))
            {
                return await LockedOut(user);
            }
            return null;
        }
        protected async Task<bool> IsLockedOut(AppUser user)
        {
            return _userManager.SupportsUserLockout && await _userManager.IsLockedOutAsync(user);
        }
        protected Task<SignInResult> LockedOut(AppUser user)
        {
            // Logger.LogDebug(EventIds.UserLockedOut, "User is currently locked out.");
            return Task.FromResult(SignInResult.LockedOut);
        }
    }
}
