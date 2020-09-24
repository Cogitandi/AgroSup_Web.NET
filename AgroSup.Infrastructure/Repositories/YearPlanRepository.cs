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
    public class YearPlanRepository : IYearPlanRepository
    {
        private readonly DatabaseContext _context;

        public YearPlanRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<YearPlan> DbInclude(DbSet<YearPlan> dbSet)
        {
            return dbSet
                .Include(x => x.User)
                .Include(x => x.Fields)
                .ThenInclude(f => f.Parcels)
                .Include(x => x.Operators);
        }

        public async Task<YearPlan> GetById(Guid id)
        {
            return await DbInclude(_context.YearPlans).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<YearPlan> GetByIdToImport(Guid id)
        {
            return await DbInclude(_context.YearPlans).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<IEnumerable<YearPlan>> GetByUser(User user)
        {
            return await DbInclude(_context.YearPlans)
                .Where(x => x.User == user)
                .ToListAsync();
        }
        public async Task<YearPlan> GetByYearBack(YearPlan yearPlan, int backYear)
        {
            var startYear = yearPlan.StartYear - backYear;
            var user = yearPlan.User;
            return await _context.YearPlans
                .Where(x=>x.User==user)
                .Where(x => x.StartYear == startYear)
                .FirstOrDefaultAsync();
        }

        public async Task Add(YearPlan yearPlan)
        {
            _context.YearPlans.Add(yearPlan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(YearPlan YearPlan)
        {
            _context.Remove(YearPlan);
            await _context.SaveChangesAsync();
        }

        public async Task Update(YearPlan YearPlan)
        {
            _context.Update(YearPlan);
            await _context.SaveChangesAsync();
        }
    }
}
