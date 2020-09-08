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

        public async Task Create(YearPlan yearPlan)
        {
            _context.YearPlans.Add(yearPlan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<YearPlan>> GetAll()
        {
            return await _context.YearPlans
                .Include(x=>x.User)
                .Include(x=>x.Fields)
                .Include(x=>x.Operators)
                .ToListAsync();
        }

        public async Task<YearPlan> GetById(Guid id)
        {
            return await _context.YearPlans
                .Include(x => x.User)
                .Include(x => x.Fields)
                .Include(x => x.Operators)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<YearPlan>> GetByUser(User user)
        {
            return await _context.YearPlans
                .Include(x => x.User)
                .Include(x => x.Fields)
                .Include(x => x.Operators)
                .Where(x => x.User == user)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Remove(YearPlan YearPlan)
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
