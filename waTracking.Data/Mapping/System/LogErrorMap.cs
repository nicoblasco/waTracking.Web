using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.System;

namespace waTracking.Data.Mapping.System
{
    public class LogErrorMap : IEntityTypeConfiguration<LogError>
    {
        public void Configure(EntityTypeBuilder<LogError> builder)
        {
            builder.ToTable("LogError")
            .HasKey(c => c.Id);

        }
    }
}
