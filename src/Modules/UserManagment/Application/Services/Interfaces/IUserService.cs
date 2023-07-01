using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CXChange.Modules.UserManagment.Domain.User;
namespace CXChange.Modules.UserManagment.Application.Services.Interfaces
{
    public  interface IUserService
    {
        void CreateUser(string PhoneNumber, string Password);
        
    }
}
