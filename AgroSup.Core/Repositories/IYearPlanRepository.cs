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
        Task<YearPlan> GetByIdToImport(Guid id);
        Task<IEnumerable<YearPlan>> GetByUser(User user);
        Task<YearPlan> GetByYearBack(YearPlan yearPlan, int yearBack);
        Task Add(YearPlan yearPlan);
        Task Update(YearPlan yearPlan);
        Task Delete(YearPlan yearPlan);
    }
}
