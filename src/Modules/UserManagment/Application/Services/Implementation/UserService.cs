using CXChange.Modules.UserManagment.Application.Services.Interfaces;
using CXChange.Modules.UserManagment.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace CXChange.Modules.UserManagment.Application.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        IHttpContextAccessor httpContextAccessor;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }


        public void CreateUser(string PhoneNumber,string Password)
        {
            _userRepository.CreateUser(new CXChange.Modules.UserManagment.Domain.User.User(PhoneNumber));
        }
        public void ConfirmUser(string PhoneNumber,string Password) { }
        public void ChangeUserPassword()
        {
            throw new NotImplementedException();
        }
        public void ResetUserPassword(string PhoneNumber) { throw new NotFiniteNumberException(); }

       
    }
}
