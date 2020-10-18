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
        Task<User> GetByName(string name);
        Task<IEnumerable<User>> GetAll();
        Task Update(User user);
        Task Delete(IEnumerable<User> users);
    }
}
