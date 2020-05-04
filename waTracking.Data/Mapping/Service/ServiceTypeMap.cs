using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Service
{
    public class ServiceTypeMap : IEntityTypeConfiguration<Entities.Service.ServiceType>
    {
        public void Configure(EntityTypeBuilder<Entities.Service.ServiceType> builder)
        {
            builder.ToTable("ServiceType")
                .HasKey(c => c.Id);
        }
    }
}
