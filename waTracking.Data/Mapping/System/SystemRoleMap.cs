using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using waTracking.Entities.System;

namespace waTracking.Data.Mapping.System
{
    public class SystemRoleMap : IEntityTypeConfiguration<SystemRole>
    {
        public void Configure(EntityTypeBuilder<SystemRole> builder)
        {
            builder.ToTable("SystemRole")
            .HasKey(c => c.Id);

        }
    }
}
