using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    class ParcelConfiguration : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Number);
            builder.Property(x => x.CultivatedArea);
            builder.Property(x => x.FuelApplication);
            builder.HasOne(x => x.Operator).WithMany(x=>x.Parcels);
        }
    }
}
