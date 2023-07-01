using CXChange.Modules.UserManagment.Domain.Interfaces.Repositories;
using CXChange.Modules.UserManagment.Domain.User;
using CXChange.Modules.UserManagment.Infrastructure.DataModel;
using CXChange.Modules.UserManagment.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Infrastructure.Repositories
{
    internal class UserRepository: IUserRepository
    {
        private IExchangeUserManager _exchangeUserManager;
        public UserRepository(IExchangeUserManager exchangeUserManager/*IExchangeSignInManager exchangeSignInManager*/)
        {
            // _exchangeSignInManager= exchangeSignInManager;
            _exchangeUserManager = exchangeUserManager;
        }

        public async Task CreateUser(User user)
        {

            SnapShot snapShot = user.GetSnapShot();
            AppUser appUser = new AppUser { UserName = snapShot.PhoneNumber, PhoneNumber = snapShot.PhoneNumber, DefaultTwoFactor = "SMS", TwoFactorEnabled = true };
            IdentityResult result = await _exchangeUserManager.CreateAsync(appUser, snapShot.Password);
            if (!result.Succeeded) { throw new Exception(result.Errors.FirstOrDefault().Description); }
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
            appuser.FirstName = snapShot.FirstName;
            appuser.LastName = snapShot.LastName;
            appuser.Email = snapShot.Email;
            appuser.UserName = snapShot.UserName;
        }

        public async Task<User> GetUserById(string id)
        {
            AppUser user=await _exchangeUserManager.GetUserById(id);
           
            return new User(user.FirstName, user.LastName, user.Email,  user.PhoneNumberConfirmed, user.TwoFactorEnabled, user.DefaultTwoFactor, user.PhoneNumber);
            
        }
    }
}
