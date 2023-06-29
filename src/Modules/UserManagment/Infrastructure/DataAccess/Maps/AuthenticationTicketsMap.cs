using CXChange.Modules.UserManagment.Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CXChange.Modules.UserManagment.Infrastructure.DataAccess.Maps
{
   
        internal class AuthenticationTicketsMap : IEntityTypeConfiguration<AuthenticationTickets>
        {
            public void Configure(EntityTypeBuilder<AuthenticationTickets> builder)
            {
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Id).HasMaxLength(128);
            }
        }
    
}
