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
    public class FertilizationTreatmentRepository : ITreatmentRepository<FertilizationTreatment>
    {
        private readonly DatabaseContext _context;

        public FertilizationTreatmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<FertilizationTreatment> DbInclude(DbSet<FertilizationTreatment> dbSet)
        {
            return dbSet
                .Include(x => x.Field)
                .ThenInclude(y => y.YearPlan)
                .Include(x => x.Fertilizer);
        }

        public async Task Add(FertilizationTreatment treatment)
        {
            _context.FertilizationTreatments.Add(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(FertilizationTreatment treatment)
        {
            _context.FertilizationTreatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(FertilizationTreatment treatment)
        {
            _context.FertilizationTreatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FertilizationTreatment>> GetAllByYearPlan(YearPlan yearPlan)
        {
            return await DbInclude(_context.FertilizationTreatments)
                .Where(x => x.Field.YearPlan == yearPlan)
                .ToListAsync();
        }

        public async Task<FertilizationTreatment> GetById(Guid id)
        {
            return await _context.FertilizationTreatments.FindAsync(id);
        }


    }
}
