using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IOperatorRepository
    {
        Task<Operator> GetById(Guid id);
        Task<IEnumerable<Operator>> GetByYearPlan(YearPlan yearplan);
        Task Add(Operator @operator);
        Task Update(Operator @operator);
        Task Delete(Operator @operator);
    }
}
