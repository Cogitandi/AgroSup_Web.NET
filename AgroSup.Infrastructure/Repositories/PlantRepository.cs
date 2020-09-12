using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroSup.Infrastructure.Repositories
{
    public class PlantRepository : IPlantRepository
    {
        private readonly DatabaseContext _context;

        public PlantRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plant>> GetAll()
        {
            return await _context.Plants
                .ToListAsync();
        }

        public async Task<Plant> GetById(Guid id)
        {
            return await _context.Plants
                .FindAsync(id);
        }

        public async Task Add(Plant plant)
        {
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Plant plant)
        {
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Plant plant)
        {
            _context.Plants.Update(plant);
            await _context.SaveChangesAsync();
        }
    }
}
