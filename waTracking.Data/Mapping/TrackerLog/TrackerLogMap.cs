using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.TrackerLog;

namespace waTracking.Data.Mapping.TrackerLog
{
    public class TrackerLogMap : IEntityTypeConfiguration<Entities.TrackerLog.TrackerLog>
    {
        public void Configure(EntityTypeBuilder<Entities.TrackerLog.TrackerLog> builder)
        {
            builder.ToTable("TrackerLog")
                .HasKey(c => c.Id);
        }
    }
}
