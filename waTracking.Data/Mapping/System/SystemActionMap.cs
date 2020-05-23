using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using waTracking.Entities.System;

namespace waTracking.Data.Mapping.System
{
   public class SystemActionMap : IEntityTypeConfiguration<SystemAction>
    {
        public void Configure(EntityTypeBuilder<SystemAction> builder)
        {
            builder.ToTable("SystemAction")
            .HasKey(c => c.Id);

        }
    }
}
