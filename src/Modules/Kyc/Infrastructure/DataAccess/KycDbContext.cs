using CXChange.Modules.Kyc.Domain.User;
using CXChange.Modules.Kyc.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CXChange.Modules.Kyc.Infrastructure.DataAccess
{
    internal class KycDbContext : DbContext
    {
        static KycDbContext()
        {

        }
        public KycDbContext(DbContextOptions<KycDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("kyc");
            modelBuilder.HasPostgresEnum<KycStatus>(schema: "kyc", name: "kyc_status");
            modelBuilder.HasPostgresEnum<KycRequestStatus>(schema: "kyc", name: "kyc_request_status");
            modelBuilder.HasPostgresEnum<StorageTypeEnum>(schema: "kyc", name: "kyc_request_storage_type");
            modelBuilder.HasPostgresEnum<LandLineStatus>(schema: "kyc", name: "kyc_land_line_status");



        }
    }
}
