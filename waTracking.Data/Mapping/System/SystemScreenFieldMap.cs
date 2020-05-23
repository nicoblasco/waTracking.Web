using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using waTracking.Entities.System;


namespace waTracking.Data.Mapping.System
{
    public class SystemScreenFieldMap : IEntityTypeConfiguration<SystemScreenField>
    {
        public void Configure(EntityTypeBuilder<SystemScreenField> builder)
        {
            builder.ToTable("SystemScreenField")
            .HasKey(c => c.Id);

        }
    }
}
