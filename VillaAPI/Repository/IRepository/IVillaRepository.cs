using System.Linq.Expressions;
using VillaAPI.Entities;

namespace VillaAPI.Repository.IRepository;

public interface IVillaRepository : IGenericRepository<Villa>
{
    Task<Villa> UpdateAsync(Villa entity);
    
}