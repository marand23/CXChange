using CXChange.Modules.Payment.Domain.Currency;
using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.Payment.Domain.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            builder.HasIndex(w => new { w.CurrencyId, w.UserId }).IsUnique();
            builder.HasOne<CurrencyConfig>()
               .WithMany()
               .HasForeignKey("CurrencyId");
            builder.ComplexProperty(x => x.Balance, b =>
            {
                b.Property(b => b.Value).HasColumnName("balance");
                b.Ignore(b => b.Currency);
            });
            builder.ComplexProperty(x => x.ReservedBalance, b =>
            {
                b.Property(b => b.Value).HasColumnName("reserved_balance");
                b.Ignore(b => b.Currency);
            });





            builder.Ignore("Currency");
            builder.HasOne<User>()
                .WithMany().HasForeignKey("UserId");


        }
    }
}
