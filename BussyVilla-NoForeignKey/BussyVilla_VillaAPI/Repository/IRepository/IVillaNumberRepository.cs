using BussyVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace BussyVilla_VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}
