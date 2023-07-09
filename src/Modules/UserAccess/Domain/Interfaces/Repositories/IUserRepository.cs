using CXChange.Modules.UserAccess.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User.User user,string Password);
        Task UpdateUser(User.User user);
        Task<User.User> GetUserById(string id);
        Task RemoveTicket(User.User user);
        Task<User.User> GetUserByPhoneNumber(string phoneNumber);
        Task<byte[]> GetUserSecurityToken(User.User user); 
    }
}
