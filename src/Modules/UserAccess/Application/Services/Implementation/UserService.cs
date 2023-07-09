using CXChange.Modules.UserAccess.Application.Services.Interfaces;
using CXChange.Modules.UserAccess.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.UserAccess.Application.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        IHttpContextAccessor httpContextAccessor;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }


       
        public void ConfirmUser(string PhoneNumber,string Password) { }
        public void ChangeUserPassword()
        {
            throw new NotImplementedException();
        }
        public void ResetUserPassword(string PhoneNumber) { throw new NotFiniteNumberException(); }

       
    }
}
