using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace AgroSup.Infrastructure.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly DatabaseContext _context;

        public FieldRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Create(Field field)
        {
            _context.Add(field);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Field>> GetAll()
        {
            return await _context.Fields
                .Include(x => x.Plant)
                .Include(x => x.YearPlan)
                .ToListAsync();
        }

        public async Task<Field> GetById(Guid id)
        {
            return await _context.Fields
                .Include(x => x.Plant)
                .Include(x => x.YearPlan)
                .Include(x => x.Parcels)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<Field>> GetByUser(User user)
        {
            return await _context.Fields
                .Include(x => x.Plant)
                .Include(x => x.YearPlan)
                .Include(x => x.Parcels)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task Remove(Field field)
        {
            _context.Remove(field);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Field field)
        {
            _context.Update(field);
            await _context.SaveChangesAsync();
        }
    }
}
