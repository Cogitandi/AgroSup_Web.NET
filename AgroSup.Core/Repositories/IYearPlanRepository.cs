using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IYearPlanRepository
    {
        Task<IEnumerable<YearPlan>> GetAll();
        Task<YearPlan> GetById(Guid id);
        Task Create(YearPlan @yearPlan);
        Task Update(YearPlan @yearPlan);
        Task Remove(YearPlan @yearPlan);
    }
}
