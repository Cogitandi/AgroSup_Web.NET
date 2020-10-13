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
    public class SprayingTreatmentRepository : ITreatmentRepository<SprayingTreatment>
    {
        private readonly DatabaseContext _context;

        public SprayingTreatmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<SprayingTreatment> DbInclude(DbSet<SprayingTreatment> dbSet)
        {
            return dbSet
                .Include(x => x.Field)
                .ThenInclude(y => y.YearPlan);
        }

        public async Task Add(SprayingTreatment treatment)
        {
            _context.SprayingTreatments.Add(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SprayingTreatment treatment)
        {
            _context.SprayingTreatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(SprayingTreatment treatment)
        {
            _context.SprayingTreatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SprayingTreatment>> GetAllByYearPlan(YearPlan yearPlan)
        {
            return await DbInclude(_context.SprayingTreatments)
                .Where(x => x.Field.YearPlan == yearPlan)
                .ToListAsync();
        }

        public async Task<SprayingTreatment> GetById(Guid id)
        {
            return await _context.SprayingTreatments.FindAsync(id);
        }


    }
}
