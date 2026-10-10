using CXChange.Modules.Payment.Domain.Currency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class CurrencyConfigMap : IEntityTypeConfiguration<CurrencyConfig>
    {
        public void Configure(EntityTypeBuilder<CurrencyConfig> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ComplexProperty(c => c.Currency, b =>
            {
                b.Property(x => x.Symbol).HasColumnName("symbol");
                b.Property(x => x.Name).HasColumnName("name");
                b.Property(x => x.Precision).HasColumnName("precision");

            });
            builder.ComplexProperty(c => c.DepositWithdrawStatus, b =>
            {
                b.Property(x => x.DepositEnable).HasColumnName("deposit_enable");
                b.Property(x => x.WithdrawEnable).HasColumnName("withdraw_enable");
            });
            //builder.OwnsMany(x => x.Networks, b =>
            //{
            //    { b.ToJson(); }
            //    { b.OwnsOne(b => b.NetworkDepositWithdrawStatus); }
            //    { b.OwnsOne(b => b.NetworkMinConfirm); }
            //    { b.OwnsOne(b => b.NetworkWithdrawLimits); }
            //    { b.OwnsOne(x => x.NetworkName); }
            //    { b.OwnsOne(x => x.NetworkRegex); }

            //    // b.ToJson();

            //});
            // context.Users.Where(u => EF.Property<int>("_phone", u).HasField("phone") == 27);

        }
    }
}
