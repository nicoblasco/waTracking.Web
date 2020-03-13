using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Data.Mapping.TrackerLog;
using waTracking.Entities.TrackerLog;

namespace waTracking.Data
{
   public class DbContextApp: DbContext
    {
        public DbSet<TrackerLog> TrackerLogs { get; set; }

        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TrackerLogMap ());

        }
    }
}
