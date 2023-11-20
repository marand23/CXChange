using CXChange.Modules.Payment.Domain.User;
using CXChange.Modules.Payment.Domain.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Payment.Infrastructure.DataAccess.Maps
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();


            builder.ComplexProperty<UserPhoneNumber>("PhoneNumber", b =>
            {
                b.Property(b => b.PhoneNumber).HasColumnName("phone_number");
            });

        }
    }
}
