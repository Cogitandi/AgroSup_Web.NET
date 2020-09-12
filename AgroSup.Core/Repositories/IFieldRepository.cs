using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IFieldRepository
    {
        Task<Field> GetById(Guid id);
        Task<IEnumerable<Field>> GetByYearPlan(YearPlan yearplan);
        Task Add(Field @field);
        Task Update(Field @field);
        Task Delete(Field @field);
    }
}
