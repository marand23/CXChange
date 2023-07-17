using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using CXChange.Modules.UserAccess.Infrastructure.Identity;
using MassTransit.Mediator;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure.Repositories
{
    internal class UserRepository: IUserRepository
    {
        private IExchangeUserManager _exchangeUserManager;
        private IExchangeSignInManager _exchangeSignInManager;
       
        private IMediator _mediator;
        public UserRepository(IExchangeUserManager exchangeUserManager,IExchangeSignInManager exchangeSignInManager,IMediator mediator)
        {
            _exchangeSignInManager = exchangeSignInManager;
            _exchangeUserManager = exchangeUserManager;
            _mediator = mediator;
        }

        public async Task CreateUser(User user, string Password)
        {

           // snapshot snapshot = user.getsnapshot();
           // foreach(var e in user.domainevents)
           // {
           //    await _mediator.publish(e);
           // }
           // appuser appuser = new appuser { username = snapshot.phonenumber, phonenumber = snapshot.phonenumber, defaulttwofactor =snapshot.defaulttwofactor, twofactorenabled = snapshot.twofactorenabled };
           
           //identityresult result = await _exchangeusermanager.createasync(appuser,password);
            
           // if (!result.Succeeded) { throw new Exception(result.Errors.FirstOrDefault().Description); }
        }
        public async Task UpdateUser(User user)
        {
            //AppUser appUser = new AppUser();
            //Map(appUser, user.GetSnapShot());

            //var result = await _exchangeUserManager.UpdateAsync(appUser);
            //if (!result.Succeeded)
            //{
            //    throw new Exception(result.Errors.FirstOrDefault().Description);
            //}
        }

        public void Map(AppUser appuser, SnapShot snapShot)
        {
            
            appuser.Email = snapShot.Email;
            appuser.UserName = snapShot.UserName;
        }

        public async Task<User> GetUserById(string id)
        {
            AppUser user=await _exchangeUserManager.GetUserById(id);

            return null;
            
        }
        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            AppUser user=await _exchangeUserManager.GetUserByPhoneNumber(phoneNumber);
            if(user==null) { return null; }
            return User.CreateUserFactory(user.Email, user.PhoneNumber, user.PhoneNumberConfirmed, user.TwoFactorEnabled, user.DefaultTwoFactor, user.UserName, user.IsRestricted);
           
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
      
    }
}
