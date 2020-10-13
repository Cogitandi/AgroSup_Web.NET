using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.Infrastructure.Repositories
{
    public class SeedingTreatmentRepository : ITreatmentRepository<SeedingTreatment>
    {
        private readonly DatabaseContext _context;

        public SeedingTreatmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<SeedingTreatment> DbInclude(DbSet<SeedingTreatment> dbSet)
        {
            return dbSet
                .Include(x => x.Field)
                .ThenInclude(y => y.YearPlan);
        }

        public async Task Add(SeedingTreatment treatment)
        {
            _context.SeedingTreatments.Add(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SeedingTreatment treatment)
        {
            _context.SeedingTreatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(SeedingTreatment treatment)
        {
            _context.SeedingTreatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SeedingTreatment>> GetAllByYearPlan(YearPlan yearPlan)
        {
            return await DbInclude(_context.SeedingTreatments)
                .Where(x => x.Field.YearPlan == yearPlan)
                .ToListAsync();
        }

        public async Task<SeedingTreatment> GetById(Guid id)
        {
            return await _context.SeedingTreatments.FindAsync(id);
        }


    }
}
