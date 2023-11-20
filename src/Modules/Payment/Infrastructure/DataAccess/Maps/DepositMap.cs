using CXChange.Modules.Payment.Domain.Deposit;
using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.Payment.Domain.Wallet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class DepositMap : IEntityTypeConfiguration<Deposit>
    {
        public void Configure(EntityTypeBuilder<Deposit> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.ComplexProperty(x => x.Address, b =>
            {
                b.Property(x => x.Address).HasColumnName("address").HasDefaultValue("");
            });
            builder.ComplexProperty(x => x.Amount, b =>
            {
                b.Property(x => x.Value).HasColumnName("amount").HasDefaultValue(0);
                b.ComplexProperty(x => x.Currency, b =>
                {
                    b.Property(x => x.Name).HasColumnName("currency");
                    b.Property(x => x.Precision).HasColumnName("precision");
                    b.Ignore(x => x.Symbol);
                    b.Ignore(x => x.Type);
                });
            });
            builder.HasOne<Wallet>()
                .WithMany()
                .HasForeignKey(x => x.WalletId);
            builder.HasOne<User>()
                .WithMany().HasForeignKey(x => x.UserId);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            builder.ComplexProperty(x => x.TrackId, b =>
            {
                b.Property(x => x.TrackId).HasColumnName("track_id").HasDefaultValue("");
            });

        }
    }
}
