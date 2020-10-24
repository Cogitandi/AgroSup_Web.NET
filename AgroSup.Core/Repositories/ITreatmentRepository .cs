using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface ITreatmentRepository
    {
        Task<Treatment> GetById(Guid id);
        Task<IEnumerable<Treatment>> GetAllByYearPlan(YearPlan yearPlan);
        Task<IEnumerable<Treatment>> GetAll();
        Task Add(Treatment treatment);
        Task AddRange(IEnumerable<Treatment> treatments);
        Task Update(Treatment treatment);
        Task Delete(Treatment treatment);
    }
}
