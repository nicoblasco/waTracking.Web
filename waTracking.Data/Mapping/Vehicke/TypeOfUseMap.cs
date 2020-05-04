using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Vehicke
{
    public class TypeOfUseMap : IEntityTypeConfiguration<Entities.Vehicle.TypeOfUse>
    {
        public void Configure(EntityTypeBuilder<Entities.Vehicle.TypeOfUse> builder)
        {
            builder.ToTable("TypeOfUse")
                .HasKey(c => c.Id);
        }
    }
}
