﻿using BussyVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace BussyVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> UpdateAsync(Villa entity);
    }
}
