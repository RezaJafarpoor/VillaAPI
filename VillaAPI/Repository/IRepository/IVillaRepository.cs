using System.Linq.Expressions;
using VillaAPI.Entities;

namespace VillaAPI.Repository.IRepository;

public interface IVillaRepository
{
    Task Create(Villa entity);
    Task Remove(Villa entity);
    Task<List<Villa>> GetAll(Expression<Func<Villa,bool>>? filter = null);
    Task<Villa> Get(Expression<Func<Villa,bool>>? filter = null, bool tracked = true);
    Task Save();
}