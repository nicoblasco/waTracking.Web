using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace waTracking.Data.Mapping.Department
{
    public class DepartmentMap : IEntityTypeConfiguration<Entities.Department.Department>
    {
        public void Configure(EntityTypeBuilder<Entities.Department.Department> builder)
        {
            builder.ToTable("Department")
                .HasKey(c => c.Id);
        }
    }
}
