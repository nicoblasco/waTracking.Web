using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Data.Mapping.Configuration
{
    public class ConfigScreenFieldMap : IEntityTypeConfiguration<ConfigScreenField>
    {
        public void Configure(EntityTypeBuilder<ConfigScreenField> builder)
        {
            builder.ToTable("ConfigScreenField")
            .HasKey(c => c.Id);
        }
    }
}
