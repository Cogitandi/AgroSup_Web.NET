using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    class YearPlanConfiguration : IEntityTypeConfiguration<YearPlan>
    {
        public void Configure(EntityTypeBuilder<YearPlan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StartYear).IsRequired();
            builder.Property(x => x.EndYear).IsRequired();
            builder.HasOne(x => x.User).WithMany(y=>y.YearPlans).IsRequired();
            builder.HasMany(x => x.Fields).WithOne(y => y.YearPlan);
            builder.HasMany(x => x.Operators).WithOne(y => y.YearPlan);
        }
    }
}
