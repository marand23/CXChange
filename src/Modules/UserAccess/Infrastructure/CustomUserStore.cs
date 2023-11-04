using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CXChange.Modules.UserAccess.Infrastructure
{

    public class CustomUserStore : UserStore<AppUser, IdentityRole<Guid>, UserManagementDbContext, Guid>
    {
        public CustomUserStore(UserManagementDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
            AutoSaveChanges = false;
        }
    }
}

