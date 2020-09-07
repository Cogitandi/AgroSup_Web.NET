using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Infrastructure.Repositories
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly DatabaseContext _context;

        public ParcelRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task Create(Parcel parcel)
        {
            _context.Parcels.Add(parcel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Parcel>> GetAll()
        {
            return await _context.Parcels
                .Include(x => x.Operator)
                .ToListAsync();
        }

        public async Task<Parcel> GetById(Guid id)
        {
            return await _context.Parcels
                .Include(x => x.Operator)
                .FirstAsync(x => x.Id.Equals(id));
        }

        public async Task Remove(Parcel parcel)
        {
            _context.Parcels.Remove(parcel);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Parcel parcel)
        {
            _context.Parcels.Update(parcel);
            await _context.SaveChangesAsync();
        }
    }
}
