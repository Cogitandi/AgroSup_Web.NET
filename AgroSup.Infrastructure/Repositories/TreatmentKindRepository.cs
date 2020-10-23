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
    public class TreatmentKindRepository : ITreatmentKindRepository
    {
        private readonly DatabaseContext _context;

        public TreatmentKindRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TreatmentKind>> GetAll()
        {
            return await _context.TreatmentKinds.ToListAsync();
        }

        public async Task<TreatmentKind> GetById(Guid id)
        {
            return await _context.TreatmentKinds.FindAsync(id);
        }

        public async Task Add(TreatmentKind treatmentKind)
        {
            _context.TreatmentKinds.Add(treatmentKind);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TreatmentKind treatmentKind)
        {
            _context.TreatmentKinds.Remove(treatmentKind);
            await _context.SaveChangesAsync();
        }

        public async Task Update(TreatmentKind treatmentKind)
        {
            _context.TreatmentKinds.Update(treatmentKind);
            await _context.SaveChangesAsync();
        }
    }
}
