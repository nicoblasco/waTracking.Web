using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Service
{
    public class ServiceMap : IEntityTypeConfiguration<Entities.Service.Service>
    {
        public void Configure(EntityTypeBuilder<Entities.Service.Service> builder)
        {
            builder.ToTable("Service")
                .HasKey(c => c.Id);
        }
    }
}
