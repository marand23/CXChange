using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using MassTransit;
using Microsoft.AspNetCore.Identity;
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
            modelBuilder.Entity<IdentityUser>().ToTable("asp_net_users");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("asp_net_user_tokens");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("asp_net_user_logins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("asp_net_user_claims");
            modelBuilder.Entity<IdentityRole>().ToTable("asp_net_roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("asp_net_user_roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("asp_net_role_claims");
        }

    }




}

