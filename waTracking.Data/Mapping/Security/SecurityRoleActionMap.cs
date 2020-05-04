using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Data.Mapping.Security
{
   public class SecurityRoleActionMap : IEntityTypeConfiguration<SecurityRoleAction>
    {
        public void Configure(EntityTypeBuilder<SecurityRoleAction> builder)
        {
            builder.ToTable("SecurityRoleAction")
                .HasKey(c => c.Id);
        }
    }
}
