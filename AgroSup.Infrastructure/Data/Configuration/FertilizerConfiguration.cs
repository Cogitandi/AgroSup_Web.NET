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
            builder.Property(x => x.Name);
            builder.Property(x => x.N);
            builder.Property(x => x.P);
            builder.Property(x => x.K);
            builder.Property(x => x.Ca);
            builder.Property(x => x.Mg);
            builder.Property(x => x.S);
            builder.Property(x => x.Na);
        }
    }
}
