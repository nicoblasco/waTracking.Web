using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Data.Mapping.Security
{
    public class SecurityRoleScreenMap : IEntityTypeConfiguration<SecurityRoleScreen>
    {
        public void Configure(EntityTypeBuilder<SecurityRoleScreen> builder)
        {
            builder.ToTable("SecurityRoleScreen")
                .HasKey(c => c.Id);
        }
    }
}
