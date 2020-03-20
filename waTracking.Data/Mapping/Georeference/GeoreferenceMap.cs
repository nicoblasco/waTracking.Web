using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Data.Mapping.Georeference
{
    public class GeoreferenceMap : IEntityTypeConfiguration<Entities.Georeference.Georeference>
    {
        public void Configure(EntityTypeBuilder<Entities.Georeference.Georeference> builder)
        {
            builder.ToTable("Georeference")
                .HasKey(c => c.Id);
        }

    }

}
