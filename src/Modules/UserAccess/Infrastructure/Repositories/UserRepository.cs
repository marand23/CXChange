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
        private IDomainEventDispatcher _domainEventDispatcher;


        public UserRepository(IExchangeUserManager exchangeUserManager, IExchangeSignInManager exchangeSignInManager, IDomainEventDispatcher domainEventDispatcher, IMediator mediator)
        {
            _exchangeSignInManager = exchangeSignInManager;
            _exchangeUserManager = exchangeUserManager;
            _domainEventDispatcher = domainEventDispatcher;

        }

        public async Task CreateUser(User user)

        {
            var snapShot = user.GetSnapShot();
            var appUser = new AppUser
            {
                UserName = snapShot.PhoneNumber,
                PhoneNumber = snapShot.PhoneNumber,
                DefaultTwoFactor = "SMS"
                ,
                TwoFactorEnabled = snapShot.TwoFactorEnabled,
                SecurityStamp = snapShot.SecurityStamp
            };

            var a = await _exchangeUserManager.CreateAsync(appUser, snapShot.Password);
            appUser.SecurityStamp = user.SecurityStamp;
            if (!a.Succeeded) { throw new InvalidOperationException(a.Errors.FirstOrDefault().Description); }


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
            AppUser user = await _exchangeUserManager.GetUserById(id);

            return null;

        }
        public async Task GetUserByPhoneNumber(string phoneNumber)
        {
            AppUser user = await _exchangeUserManager.GetUserByPhoneNumber(phoneNumber);

            //  return User.CreateUserFactory(user.Email, user.PhoneNumber, user.PhoneNumberConfirmed, user.TwoFactorEnabled, user.DefaultTwoFactor, user.UserName, user.IsRestricted);

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
