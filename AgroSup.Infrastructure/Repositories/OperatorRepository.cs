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

        public async Task Create(Operator @operator)
        {
            _context.Operators.Add(@operator);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Operator>> GetAll()
        {
            return await _context.Operators
                .Include(x => x.YearPlan)
                .Include(x => x.Parcels)
                .ToListAsync();

        }

        public async Task<Operator> GetById(Guid id)
        {
            return await _context.Operators
                .Include(x => x.YearPlan)
                .Include(x => x.Parcels)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Operator>> GetByUser(User user)
        {
            return await _context.Operators
                .Include(x => x.YearPlan)
                .Include(x => x.Parcels)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Remove(Operator @operator)
        {
            _context.Operators.Remove(@operator);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Operator @operator)
        {
            _context.Operators.Update(@operator);
            await _context.SaveChangesAsync();
        }
    }
}
