using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using waTracking.Entities.System;

namespace waTracking.Data.Mapping.System
{
   public  class SystemScreenMap : IEntityTypeConfiguration<SystemScreen>
    {
        public void Configure(EntityTypeBuilder<SystemScreen> builder)
        {
            builder.ToTable("SystemScreen")
            .HasKey(c => c.Id) ;

        }
    }
}
