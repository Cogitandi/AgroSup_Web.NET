using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    class FertilizerConfiguration : IEntityTypeConfiguration<Fertilizer>
    {
        public void Configure(EntityTypeBuilder<Fertilizer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.N).IsRequired();
            builder.Property(x => x.P).IsRequired();
            builder.Property(x => x.K).IsRequired();
            builder.Property(x => x.Ca).IsRequired();
            builder.Property(x => x.Mg).IsRequired();
            builder.Property(x => x.S).IsRequired();
            builder.Property(x => x.Na).IsRequired();
        }
    }
}
