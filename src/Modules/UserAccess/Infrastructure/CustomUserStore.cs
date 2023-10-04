using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class CustomUSerStore : UserStore<AppUser>
    {

        public CustomUSerStore(UserManagementDbContext context)
        : base(context)
        {

            AutoSaveChanges = false;
        }

    }
}

