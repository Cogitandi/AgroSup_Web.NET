using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    public class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number);
            builder.Property(x => x.Name);
            builder.Property(x => x.PlantVariety);
            builder.HasOne(x => x.Plant);
            builder.HasOne(x => x.YearPlan).WithMany(y => y.Fields);
            builder.HasMany(x => x.Parcels).WithOne(y => y.Field);


        }
    }
}
