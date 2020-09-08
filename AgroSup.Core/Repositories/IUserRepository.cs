using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task Update(User user);
    }
}
