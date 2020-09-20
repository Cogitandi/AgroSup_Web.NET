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

        private IQueryable<Field> DbInclude(DbSet<Field> dbSet)
        {
            return dbSet
                .Include(x => x.Plant)
                .Include(x => x.Parcels)
                .ThenInclude(y => y.Operator)
                .Include(x => x.YearPlan);
        }

        public async Task Add(Field field)
        {
            _context.Add(field);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Field field)
        {
            _context.Remove(field);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Field field)
        {
            _context.Update(field);
            await _context.SaveChangesAsync();
        }

        public async Task<Field> GetById(Guid id)
        {
            return await DbInclude(_context.Fields).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Field> GetPrevious(Guid id)
        {
            Field previousField = null;
            var fieldFromId = await GetById(id);
            var fields = await GetByYearPlan(fieldFromId.YearPlan);
            var fieldsList = fields.ToList();
            for(int i=0;i< fieldsList.Count();i++)
            {
                if(fieldsList[i].Id == id)
                {
                    previousField = fieldsList[i - 1];
                }
            }
            return previousField;
        }
        public async Task<Field> GetNext(Guid id)
        {
            Field nextField = null;
            var fieldFromId = await GetById(id);
            var fields = await GetByYearPlan(fieldFromId.YearPlan);
            var fieldsList = fields.ToList();
            for (int i = 0; i < fieldsList.Count(); i++)
            {
                if (fieldsList[i].Id == id)
                {
                    nextField = fieldsList[i + 1];
                }
            }
            return nextField;
        }

        public async Task<IEnumerable<Field>> GetByYearPlan(YearPlan yearplan)
        {
            return await DbInclude(_context.Fields)
                .Where(x => x.YearPlan == yearplan)
                .OrderBy(x=>x.Number)
                .ToListAsync();
        }
    }
}
