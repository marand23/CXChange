using CXChange.Modules.Payment.Domain.BankCard;
using CXChange.Modules.Payment.Domain.BankCard.Enums;
using CXChange.Modules.Payment.Domain.Currency;
using CXChange.Modules.Payment.Domain.Deposit;
using CXChange.Modules.Payment.Domain.Deposit.Enums;
using CXChange.Modules.Payment.Domain.Shaba;
using CXChange.Modules.Payment.Domain.Shaba.Enums;
using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.Payment.Domain.Wallet;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess
{
    internal class PaymentDbContext : DbContext
    {

        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CurrencyConfig> CurrencyConfigs { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<Shaba> Shabas { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasDefaultSchema("payment");
            modelBuilder.HasPostgresEnum<BankCardStatus>(schema: "payment", name: "bankcard_status");
            modelBuilder.HasPostgresEnum<ShabaStatus>(schema: "payment", name: "shaba_status");
            modelBuilder.HasPostgresEnum<DepositStatus>(schema: "payment", name: "deposit_status");

        }
    }
}
