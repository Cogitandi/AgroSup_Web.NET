using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.DosePerHa);
            builder.Property(x => x.Composition);
            builder.Property(x => x.ReasonForUse);
            builder.HasOne(x => x.Field);
            builder.HasOne(x => x.Fertilizer);
            builder.HasOne(x => x.TreatmentKind);
        }
    }
}
