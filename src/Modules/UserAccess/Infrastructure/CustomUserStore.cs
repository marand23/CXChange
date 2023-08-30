using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class CustomUSerStore : UserStore<AppUser>
    {
        public CustomUSerStore(UserManagementDbContext context)
        : base(context)
        {
            AutoSaveChanges = false;
        }
        public async Task<AppUser> FindByPhoneNumberAsync(string phoneNumber)
        {
            // You can use any data access logic here to query the database
            // For example, using Entity Framework Core
            return await Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
        }
    }
}

