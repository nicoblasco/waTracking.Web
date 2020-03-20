using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Data.Mapping.GeoTracker
{
    public class GeoTrackerMap : IEntityTypeConfiguration<Entities.GeoTracker.GeoTracker>
    {
        public void Configure(EntityTypeBuilder<Entities.GeoTracker.GeoTracker> builder)
        {
            builder.ToTable("GeoTracker")
                .HasKey(c => c.Id);
        }

    }
}
