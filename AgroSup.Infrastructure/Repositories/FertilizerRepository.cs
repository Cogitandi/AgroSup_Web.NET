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
    public class FertilizerRepository : IFertilizerRepository
    {
        private readonly DatabaseContext _context;

        public FertilizerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Fertilizer>> GetAll()
        {
            return await _context.Fertilizers
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Fertilizer> GetById(Guid id)
        {
            return await _context.Fertilizers
                .FindAsync(id);
        }

        public async Task Add(Fertilizer fertilizer)
        {
            _context.Fertilizers.Add(fertilizer);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Fertilizer fertilizer)
        {
            _context.Fertilizers.Remove(fertilizer);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Fertilizer fertilizer)
        {
            _context.Fertilizers.Update(fertilizer);
            await _context.SaveChangesAsync();
        }



    }
}
