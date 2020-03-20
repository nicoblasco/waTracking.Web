using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Data.Mapping.Security
{
    public class SecurityRoleMap : IEntityTypeConfiguration<SecurityRole>
    {
        public void Configure(EntityTypeBuilder<SecurityRole> builder)
        {
            builder.ToTable("SecurityRole")
                .HasKey(c => c.Id);
        }
    }
}
