using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;

namespace CXChange.Modules.UserAccess.Infrastructure.Identity
{
    internal interface IExchangeSignInManager
    {
        Task<SignInResult> ExchangeSignIn(AppUser user, string password, bool lockoutOnFailure);
        Task<SignInResult> ExchangeTwoFactorSignInAsync(AppUser user, string provider, string code, string purpose);
        Task<SignInResult> ExchangeSignIn(AppUser user);
        Task LogOut();
    }
}
