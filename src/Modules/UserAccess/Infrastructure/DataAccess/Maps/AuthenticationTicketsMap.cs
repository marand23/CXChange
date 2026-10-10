using CXChange.Modules.UserAccess.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.UserAccess.Infrastructure.DataAccess.Maps
{

    internal class AuthenticationTicketsMap : IEntityTypeConfiguration<AuthenticationTickets>
    {
        public void Configure(EntityTypeBuilder<AuthenticationTickets> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasMaxLength(128);
            builder.Property(t => t.Version).IsConcurrencyToken().HasDefaultValue(0);
            builder.Property(t => t.increment).UseIdentityAlwaysColumn();

        }
    }

}
