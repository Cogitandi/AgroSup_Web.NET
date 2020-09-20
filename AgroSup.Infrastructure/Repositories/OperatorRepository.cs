using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AgroSup.Infrastructure.Data
{
    public class OperatorRepository : IOperatorRepository
    {
        private readonly DatabaseContext _context;

        public OperatorRepository(DatabaseContext context)
        {
            _context = context;
        }

        private IQueryable<Operator> DbInclude(DbSet<Operator> dbSet)
        {
            return dbSet
                .Include(x => x.Parcels)
                .ThenInclude(x => x.Field);
        }

        public async Task Add(Operator @operator)
        {
            _context.Operators.Add(@operator);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Operator @operator)
        {
            _context.Operators.Remove(@operator);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Operator @operator)
        {
            _context.Operators.Update(@operator);
            await _context.SaveChangesAsync();
        }

        public async Task<Operator> GetById(Guid id)
        {
            return await DbInclude(_context.Operators).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Operator>> GetByYearPlan(YearPlan yearplan)
        {
            return await DbInclude(_context.Operators)
                .Where(x => x.YearPlan == yearplan)
                .ToListAsync();
        }
    }
}
