using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using waTracking.Entities.Configuration;

namespace waTracking.Data.Mapping.Configuration
{
    public class ConfigScreenMap : IEntityTypeConfiguration<ConfigScreen>
    {
        public void Configure(EntityTypeBuilder<ConfigScreen> builder)
        {
            builder.ToTable("ConfigScreen")
            .HasKey(c => c.Id);


            //builder.HasMany(a => a.ConfigScreenFields)
            //       .WithOne(e=>e.ConfigScreen)
            //       .HasForeignKey<ConfigScreenField>(b => b.ConfigScreen);


        }
    }
}
