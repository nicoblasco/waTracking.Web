using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Vehicke
{
    public class VehicleSegmentMap : IEntityTypeConfiguration<Entities.Vehicle.VehicleSegment>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.VehicleSegment> builder)
        {
            builder.ToTable("VehicleSegment")
                .HasKey(c => c.Id);
        }
    }
}
