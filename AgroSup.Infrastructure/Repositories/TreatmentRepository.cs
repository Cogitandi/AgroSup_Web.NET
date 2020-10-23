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
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly DatabaseContext _context;

        public TreatmentRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<Treatment> DbInclude(DbSet<Treatment> dbSet)
        {
            return dbSet
                .Include(x => x.Field)
                .ThenInclude(y => y.YearPlan)
                .Include(x => x.TreatmentKind)
                .Include(x => x.Fertilizer);
        }

        public async Task Add(Treatment treatment)
        {
            _context.Treatments.Add(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Treatment> treatments)
        {
            _context.Treatments.AddRange(treatments);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Treatment treatment)
        {
            _context.Treatments.Remove(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Treatment treatment)
        {
            _context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Treatment>> GetAllByYearPlan(YearPlan yearPlan)
        {
            return await DbInclude(_context.Treatments)
                .Where(x => x.Field.YearPlan == yearPlan)
                .ToListAsync();
        }

        public async Task<Treatment> GetById(Guid id)
        {
            return await _context.Treatments.FindAsync(id);
        }


    }
}
