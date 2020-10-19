using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    public class SprayingTreatmentConfiguration : IEntityTypeConfiguration<SprayingTreatment>
    {
        public void Configure(EntityTypeBuilder<SprayingTreatment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Date);
            builder.HasOne(x => x.Field);
            builder.Property(x => x.Notes);
            builder.Property(x => x.Composition);
            builder.Property(x => x.ReasonForUse);
            builder.Property(x => x.Comments);
        }
    }
}
