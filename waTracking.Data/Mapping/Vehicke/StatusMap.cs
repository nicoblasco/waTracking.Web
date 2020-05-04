using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Vehicke
{
    public class StatusMap : IEntityTypeConfiguration<Entities.Vehicle.Status>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.Status> builder)
        {
            builder.ToTable("Status")
                .HasKey(c => c.Id);
        }
    }
}
