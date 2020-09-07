using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IParcelRepository
    {
        Task<IEnumerable<Parcel>> GetAll();
        Task<Parcel> GetById(Guid id);
        Task Create(Parcel @parcel);
        Task Update(Parcel @parcel);
        Task Remove(Parcel @parcel);
    }
}
