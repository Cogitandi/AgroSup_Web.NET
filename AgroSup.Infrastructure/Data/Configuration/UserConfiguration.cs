using AgroSup.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(x => x.ManagedYearPlan);
            builder.HasMany(x => x.ChoosedPlants).WithOne(y => y.User);
            builder.HasMany(x => x.YearPlans).WithOne(y => y.User);
            builder.Property(x => x.CreateDate);
            builder.Property(x => x.LastLoginDate);
        }
    }
}
