using CXChange.Modules.Kyc.Domain.KycRequest.Enums;
using CXChange.Modules.Kyc.Domain.User.Enums;
using CXChange.Modules.Kyc.Infrastructure.DataModel;
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
        public DbSet<UserDataModel> Users { get; set; }
        public DbSet<KycRequestDto> KycRequests { get; set; }
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
