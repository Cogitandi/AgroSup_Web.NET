using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface ITreatmentRepository<T>
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllByYearPlan(YearPlan yearPlan);
        Task Add(T treatment);
        Task Update(T treatment);
        Task Delete(T treatment);
    }
}
