using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.System;

namespace waTracking.Data.Mapping.System
{
   public class SystemRoleScreenMap : IEntityTypeConfiguration<SystemRoleScreen>
    {
        public void Configure(EntityTypeBuilder<SystemRoleScreen> builder)
        {
            builder.ToTable("SystemRoleScreen")
            .HasKey(c => c.Id);

        }
    }
}
