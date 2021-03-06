﻿using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroSup.Core.Repositories
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAll();
        Task<Plant> GetById(Guid id);
        Task Add(Plant plant);
        Task Update(Plant plant);
        Task Delete(Plant plant);
    }
}
