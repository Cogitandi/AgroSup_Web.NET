using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IFertilizerRepository
    {
        Task<IEnumerable<Fertilizer>> GetAll();
        Task<Fertilizer> GetById(Guid id);
        Task Add(Fertilizer fertilizer);
        Task Update(Fertilizer fertilizer);
        Task Delete(Fertilizer fertilizer);
    }
}
