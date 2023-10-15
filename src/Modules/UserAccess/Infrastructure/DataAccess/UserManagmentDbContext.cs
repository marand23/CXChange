using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using MassTransit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Infrastructure.DataAccess
{
    public class UserManagementDbContext : IdentityDbContext<AppUser>
    {



        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        {

        }


        public DbSet<AuthenticationTickets> AuthenticationTickets { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("UserAccess");
            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

        }

    }




}

