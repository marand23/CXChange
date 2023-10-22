using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CXChange.Modules.Kyc.Infrastructure.DataAccess
{
    internal class KycDbContext : DbContext
    {
        public KycDbContext(DbContextOptions<KycDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.HasDefaultSchema("user_access");

        }
    }
}
