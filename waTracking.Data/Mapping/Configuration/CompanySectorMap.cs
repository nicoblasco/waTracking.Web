using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Data.Mapping.Configuration
{
    public class CompanySectorMap : IEntityTypeConfiguration<CompanySector>
    {
        public void Configure(EntityTypeBuilder<CompanySector> builder)
        {
            builder.ToTable("CompanySector")
            .HasKey(c => c.Id);
        }
    }
}
