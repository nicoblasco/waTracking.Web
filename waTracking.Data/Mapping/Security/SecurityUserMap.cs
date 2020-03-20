using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Data.Mapping.Security
{
    public class SecurityUserMap : IEntityTypeConfiguration<SecurityUser>
    {
        public void Configure(EntityTypeBuilder<SecurityUser> builder)
        {
            builder.ToTable("SecurityUser")
            .HasKey(c => c.Id);
        }
    }
}
