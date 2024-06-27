using System.Linq.Expressions;
using VillaAPI.Entities;

namespace VillaAPI.Repository.IRepository;

public interface IGenericRepository<T> where T: class
{
    
    Task CreateAsync(T entity);
    Task Remove(T entity);
    Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null);
    Task<T> GetAsync(Expression<Func<T,bool>> filter = null, bool tracked = true);
    Task Save();
}