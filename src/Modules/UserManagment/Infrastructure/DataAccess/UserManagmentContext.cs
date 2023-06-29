using CXChange.Modules.UserManagment.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CXChange.Modules.UserManagment.Infrastructure.DataAccess
{
    internal class UserManagementDbContext : IdentityDbContext<AppUser>
    {



        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        { }

        public DbSet<AuthenticationTickets> AuthenticationTickets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("UserManagement");

        }




    }
}
