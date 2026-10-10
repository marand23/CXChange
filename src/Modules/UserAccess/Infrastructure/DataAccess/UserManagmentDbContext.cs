using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CXChange.Modules.UserAccess.Infrastructure.DataAccess
{
    public class UserManagementDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {


        //    static UserManagementDbContext()
        //=> NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("user_access.active_two_factor");
        public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
            : base(options)
        {
            //NpgsqlConnection.GlobalTypeMapper.MapEnum<TwoFactorEnum>("active_two_factor");
        }


        public DbSet<AuthenticationTickets> AuthenticationTickets { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresEnum<TwoFactorEnum>(schema: "user_access", name: "active_two_factor");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("user_access");
            modelBuilder.Entity<AppUser>().ToTable("asp_net_users");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("asp_net_user_tokens");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("asp_net_user_logins");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("asp_net_user_claims");
            // modelBuilder.Entity<IdentityRole>().ToTable("asp_net_roles");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("asp_net_user_roles");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("asp_net_role_claims");

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

        }

    }




}

