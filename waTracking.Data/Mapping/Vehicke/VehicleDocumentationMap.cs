using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace waTracking.Data.Mapping.Vehicke
{
    public class VehicleDocumentationMap : IEntityTypeConfiguration<Entities.Vehicle.VehicleDocumentation>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.VehicleDocumentation> builder)
        {
            builder.ToTable("VehicleDocumentation")
                .HasKey(c => c.Id);
        }
    }
}
