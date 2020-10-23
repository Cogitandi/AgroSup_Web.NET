using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface ITreatmentKindRepository
    {
        Task<TreatmentKind> GetById(Guid id);
        Task<IEnumerable<TreatmentKind>> GetAll();
        Task Add(TreatmentKind treatment);
        Task Update(TreatmentKind treatment);
        Task Delete(TreatmentKind treatment);
    }
}
