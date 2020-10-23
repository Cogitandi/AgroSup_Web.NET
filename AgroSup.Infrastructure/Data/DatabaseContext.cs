using AgroSup.Core.Domain;
using AgroSup.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Infrastructure.Data
{
    public class DatabaseContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<UserPlant> UserPlants { get; set; }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<YearPlan> YearPlans { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }
        public DbSet<TreatmentKind> TreatmentKinds { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new FieldConfiguration());
            builder.ApplyConfiguration(new OperatorConfiguration());
            builder.ApplyConfiguration(new ParcelConfiguration());
            builder.ApplyConfiguration(new PlantConfiguration());
            builder.ApplyConfiguration(new UserPlantConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new YearPlanConfiguration());
            builder.ApplyConfiguration(new FertilizerConfiguration());
            builder.ApplyConfiguration(new TreatmentKindConfiguration());
            builder.ApplyConfiguration(new TreatmentConfiguration());
        }
    }
}
