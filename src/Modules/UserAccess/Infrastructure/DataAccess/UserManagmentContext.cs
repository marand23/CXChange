using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MassTransit;
namespace CXChange.Modules.UserAccess.Infrastructure.DataAccess
{
    public class UserManagementDbContext : IdentityDbContext<AppUser>
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
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

        }




    }
}
