using CXChange.Modules.Kyc.Domain.User;
using CXChange.Modules.Kyc.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Kyc.Infrastructure.DataAccess.Maps
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.Property("LandLine");
            builder.Property("KycStatus");
            builder.Property("LandLineStatus");
            builder.Property("BirthDate");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property("Version").IsConcurrencyToken().HasDefaultValue(1);
            builder.Property("CreatedAt").HasDefaultValueSql("now()");
            builder.OwnsOne<KycRequest>("UserKycRequest", b =>
            {
                b.ToTable("kyc_request");
                b.Property(p => p.CreatedAt).HasDefaultValueSql("now()");
                b.WithOwner().HasForeignKey("UserId");

                b.OwnsOne<FullName>("FullName", b =>
                {
                    b.Property(b => b.SurName).HasColumnName("sur_name");
                    b.Property(b => b.Name).HasColumnName("name");
                });
                b.OwnsOne<NationalId>("NationalId", b =>
                {

                    b.Property(b => b.NationalNumber).HasColumnName("national_id");
                    b.HasIndex(b => b.NationalNumber);
                });



            });
            builder.ComplexProperty<FullName>("FullName", b =>
            {
                b.Property(b => b.SurName).HasColumnName("sur_name");
                b.Property(b => b.Name).HasColumnName("name");
            });
            builder.ComplexProperty<NationalId>("NationalId", b =>
            {

                b.Property(b => b.NationalNumber).HasColumnName("national_id");

            });








        }
    }
}
