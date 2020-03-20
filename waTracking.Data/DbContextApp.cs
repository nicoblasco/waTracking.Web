using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Data.Mapping.Georeference;
using waTracking.Data.Mapping.GeoTracker;
using waTracking.Data.Mapping.Security;
using waTracking.Data.Mapping.TrackerLog;
using waTracking.Entities.Georeference;
using waTracking.Entities.GeoTracker;
using waTracking.Entities.Security;
using waTracking.Entities.TrackerLog;

namespace waTracking.Data
{
   public class DbContextApp: DbContext
    {
        public DbSet<TrackerLog> TrackerLogs { get; set; }
        public DbSet<GeoTracker> GeoTrackers { get; set; }
        public DbSet<Georeference> Georeferences { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }

        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new TrackerLogMap ());
            modelBuilder.ApplyConfiguration(new GeoreferenceMap());
            modelBuilder.ApplyConfiguration(new GeoTrackerMap());
            modelBuilder.ApplyConfiguration(new SecurityRoleMap());
            modelBuilder.ApplyConfiguration(new SecurityUserMap());

        }
    }
}
