using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IYearPlanRepository
    {
        Task<YearPlan> GetById(Guid id);
        Task<IEnumerable<YearPlan>> GetByUser(User user);
        Task Add(YearPlan yearPlan);
        Task Update(YearPlan yearPlan);
        Task Delete(YearPlan yearPlan);
    }
}
