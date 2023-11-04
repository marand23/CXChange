using CXChange.Modules.Kyc.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Kyc.Infrastructure.DataAccess.Maps
{
    internal class AuthenticationTicketsMap : IEntityTypeConfiguration<KycRequestDto>
    {
        public void Configure(EntityTypeBuilder<KycRequestDto> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId).ValueGeneratedNever();


        }
    }
}
