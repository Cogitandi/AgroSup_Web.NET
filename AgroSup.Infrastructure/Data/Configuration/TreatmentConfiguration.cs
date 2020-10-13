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
        }
    }
}
