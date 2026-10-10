using CXChange.Modules.Payment.Domain.BankCard;
using CXChange.Modules.Payment.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class BankCardMap : IEntityTypeConfiguration<BankCard>
    {
        public void Configure(EntityTypeBuilder<BankCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            //builder.HasIndex(w => w.CardNumber.Address).IsUnique();
            builder.ComplexProperty(w => w.CardNumber, b =>
            {

                b.Property(x => x.Address).HasColumnName("card_number");
            });
            builder.ComplexProperty(w => w.BankName, b =>
            {
                b.Property(x => x.Name).HasColumnName("bank_name");
            });
            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(x => x.UserId);







        }
    }
}
