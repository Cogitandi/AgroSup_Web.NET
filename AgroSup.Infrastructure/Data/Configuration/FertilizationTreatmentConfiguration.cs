using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    public class FertilizationTreatmentConfiguration : IEntityTypeConfiguration<FertilizationTreatment>
    {
        public void Configure(EntityTypeBuilder<FertilizationTreatment> builder)
        {
            builder.HasOne(x => x.Fertilizer);
        }
    }
}
