using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserManagment.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUser(User.User user);
        Task UpdateUser(User.User user);
    }
}
