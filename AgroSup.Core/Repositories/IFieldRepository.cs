using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IFieldRepository
    {
        Task<IEnumerable<Field>> GetAll();
        Task<Field> GetById(Guid id);
        Task<IEnumerable<Field>> GetByUser(User user);
        Task Create(Field @field);
        Task Update(Field @field);
        Task Remove(Field @field);
    }
}
