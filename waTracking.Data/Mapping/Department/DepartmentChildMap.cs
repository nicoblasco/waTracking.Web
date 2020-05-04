using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace waTracking.Data.Mapping.Department
{
    public class DepartmentChildMap : IEntityTypeConfiguration<Entities.Department.DepartmentChild>
    {
        public void Configure(EntityTypeBuilder<Entities.Department.DepartmentChild> builder)
        {
            builder.ToTable("DepartmentChild")
                .HasKey(c => c.Id);
        }
    }
}
