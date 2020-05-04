using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Vehicke
{
    public class VehicleModelMap : IEntityTypeConfiguration<Entities.Vehicle.VehicleModel>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.VehicleModel> builder)
        {
            builder.ToTable("VehicleModel")
                .HasKey(c => c.Id);
        }
    }
}
