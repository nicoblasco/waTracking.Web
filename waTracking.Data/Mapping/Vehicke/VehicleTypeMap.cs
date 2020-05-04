using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Vehicke
{
    public class VehicleTypeMap : IEntityTypeConfiguration<Entities.Vehicle.VehicleType>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.VehicleType> builder)
        {
            builder.ToTable("VehicleType")
                .HasKey(c => c.Id);
        }
    }
}
