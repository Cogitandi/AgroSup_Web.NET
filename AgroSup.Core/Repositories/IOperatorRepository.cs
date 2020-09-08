using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IOperatorRepository
    {
        Task<IEnumerable<Operator>> GetAll();
        Task<Operator> GetById(Guid id);
        Task<IEnumerable<Operator>> GetByUser(User user);
        Task Create(Operator @operator);
        Task Update(Operator @operator);
        Task Remove(Operator @operator);
    }
}
