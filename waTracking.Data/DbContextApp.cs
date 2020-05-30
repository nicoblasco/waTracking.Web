using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Data.Mapping.Configuration;
using waTracking.Data.Mapping.Department;
using waTracking.Data.Mapping.Georeference;
using waTracking.Data.Mapping.GeoTracker;
using waTracking.Data.Mapping.Security;
using waTracking.Data.Mapping.Service;
using waTracking.Data.Mapping.System;
using waTracking.Data.Mapping.TrackerLog;
using waTracking.Data.Mapping.Vehicke;
using waTracking.Entities.Configuration;
using waTracking.Entities.Department;
using waTracking.Entities.Georeference;
using waTracking.Entities.GeoTracker;
using waTracking.Entities.Security;
using waTracking.Entities.Service;
using waTracking.Entities.System;
using waTracking.Entities.TrackerLog;
using waTracking.Entities.Vehicle;

namespace waTracking.Data
{
   public class DbContextApp: DbContext
    {
        public DbSet<TrackerLog> TrackerLogs { get; set; }
        public DbSet<GeoTracker> GeoTrackers { get; set; }
        public DbSet<Georeference> Georeferences { get; set; }
        public DbSet<SecurityUser> SecurityUsers { get; set; }
        public DbSet<SecurityRole> SecurityRoles { get; set; }
        public DbSet<SecurityAction> SecurityActions { get; set; }
        public DbSet<SecurityRoleAction> SecurityRoleActions { get; set; }
        public DbSet<VehicleBrand> VehicleBrands { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<TypeOfUse> TypeOfUses { get; set; }
        public DbSet<VehicleDocumentation> VehicleDocumentations { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleSegment> VehicleSegments { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ConfigScreen> ConfigScreens { get; set; }
        public DbSet<ConfigScreenField> ConfigScreenFields { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentChild> DepartmentChilds { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        public DbSet<SystemAction> SystemAction { get; set; }
        public DbSet<SystemRole> SystemRole { get; set; }
        public DbSet<SystemRoleAction> SystemRoleAction { get; set; }
        public DbSet<SystemScreen> SystemScreen { get; set; }
        public DbSet<SystemScreenField> SystemScreenField { get; set; }
        public DbSet<SystemRoleScreen> SystemRoleScreen { get; set; }




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
            modelBuilder.ApplyConfiguration(new SecurityActionMap());
            modelBuilder.ApplyConfiguration(new SecurityRoleActionMap());
            modelBuilder.ApplyConfiguration(new VehicleBrandMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new ConfigScreenMap());
            modelBuilder.ApplyConfiguration(new ConfigScreenFieldMap());
            modelBuilder.ApplyConfiguration(new DepartmentMap());
            modelBuilder.ApplyConfiguration(new DepartmentChildMap());
            modelBuilder.ApplyConfiguration(new ServiceMap());
            modelBuilder.ApplyConfiguration(new ServiceTypeMap());
            modelBuilder.ApplyConfiguration(new StatusMap());
            modelBuilder.ApplyConfiguration(new TypeOfUseMap());
            modelBuilder.ApplyConfiguration(new VehicleDocumentationMap());
            modelBuilder.ApplyConfiguration(new VehicleModelMap());
            modelBuilder.ApplyConfiguration(new VehicleSegmentMap());
            modelBuilder.ApplyConfiguration(new VehicleTypeMap());
            modelBuilder.ApplyConfiguration(new SystemActionMap());
            modelBuilder.ApplyConfiguration(new SystemRoleMap());
            modelBuilder.ApplyConfiguration(new SystemRoleActionMap());
            modelBuilder.ApplyConfiguration(new SystemScreenMap());
            modelBuilder.ApplyConfiguration(new SystemScreenFieldMap());
            modelBuilder.ApplyConfiguration(new SystemRoleScreenMap());

        }
    }
}
