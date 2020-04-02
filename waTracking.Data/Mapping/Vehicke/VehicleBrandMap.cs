using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace waTracking.Data.Mapping.Vehicke
{
    public class VehicleBrandMap : IEntityTypeConfiguration<Entities.Vehicle.VehicleBrand>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.VehicleBrand> builder)
        {
            builder.ToTable("VehicleBrand")
                .HasKey(c => c.Id);
        }
    }
}
