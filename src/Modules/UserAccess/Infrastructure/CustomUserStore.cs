using CXChange.Modules.UserAccess.Infrastructure.DataAccess;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CXChange.Modules.UserAccess.Infrastructure
{
    internal class CustomUSerStore:UserStore<AppUser>
    {
        public CustomUSerStore(UserManagementDbContext context)
        : base(context)
        {
            AutoSaveChanges = false;
        }
    }
}
