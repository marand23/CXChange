using CXChange.Modules.Payment.Domain.Shaba;
using CXChange.Modules.Payment.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class ShabaMap : IEntityTypeConfiguration<Shaba>
    {
        public void Configure(EntityTypeBuilder<Shaba> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            // builder.HasIndex(x => x.ShabaNumber.Address).IsUnique();
            builder.ComplexProperty(w => w.ShabaNumber, b =>
            {

                b.Property(x => x.Address).HasColumnName("shaba_number");

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
