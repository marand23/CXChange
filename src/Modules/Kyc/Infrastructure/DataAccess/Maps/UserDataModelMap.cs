using CXChange.Modules.Kyc.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.Kyc.Infrastructure.DataAccess.Maps
{
    internal class UserDataModelMap : IEntityTypeConfiguration<UserDataModel>
    {
        public void Configure(EntityTypeBuilder<UserDataModel> builder)
        {
            ;
            builder.HasIndex(x => x.NationlId).IsUnique();
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
