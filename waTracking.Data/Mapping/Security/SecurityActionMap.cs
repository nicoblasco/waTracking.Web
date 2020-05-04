using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Security;

namespace waTracking.Data.Mapping.Security
{
    public class SecurityActionMap : IEntityTypeConfiguration<SecurityAction>
    {
        public void Configure(EntityTypeBuilder<SecurityAction> builder)
        {
            builder.ToTable("SecurityAction")
                .HasKey(c => c.Id);
        }
    }
}
