using AgroSup.Core.Domain;
using AgroSup.Core.Repositories;
using AgroSup.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgroSup.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.ApplicationUsers
                 .Include(x => x.YearPlans)
                 .ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.ApplicationUsers
                .Include(x => x.ManagedYearPlan)
                .ThenInclude(x=>x.Operators)
                .Include(x=>x.ChoosedPlants)
                .ThenInclude(x=>x.Plant)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetByName(string name)
        {
            return await _context.ApplicationUsers
                .Include(x => x.ManagedYearPlan)
                .ThenInclude(x => x.Operators)
                .Include(x => x.ChoosedPlants)
                .ThenInclude(x => x.Plant)
                .FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task Update(User user)
        {
            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(IEnumerable<User> users)
        {
            _context.ApplicationUsers.RemoveRange(users);
            await _context.SaveChangesAsync();
        }

    }
}
