using CXChange.Modules.UserAccess.Domain.User;
using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.UserAccess.Infrastructure.DataAccess.Maps
{
    internal class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.ActiveTwoFactor).HasDefaultValue(TwoFactorEnum.SMS);
            builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

        }
    }
}
