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

        public async Task<User> GetById(Guid id)
        {
            return await _context.ApplicationUsers
                .Include(x => x.ManagedYearPlan)
                .ThenInclude(x=>x.Operators)
                .FirstAsync(x => x.Id == id);
        }

        public async Task Update(User user)
        {
            _context.ApplicationUsers.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
