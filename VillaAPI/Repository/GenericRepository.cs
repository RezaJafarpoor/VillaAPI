using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.Entities;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository;

public class GenericRepository<T> : IGenericRepository<T>  where T : class
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<T> DbSet;

    public GenericRepository( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        this.DbSet = dbContext.Set<T>();
    }
    public async Task CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await Save();
    }

    public async Task Remove(T entity)
    {
        DbSet.Remove(entity);
        await Save();
    }

    

    public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>>? filter = null)
    {
        IQueryable<T> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);

        }

        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T,bool>> filter = null, bool tracked = true)
    {
        IQueryable<T> query = DbSet;
        if (!tracked)
        {
            query.AsNoTracking();
        }

        if (filter != null)
        {
            query= query.Where(filter);
        }


        return await query.FirstOrDefaultAsync();
    }

    public async Task Save()
    {
        await _dbContext.SaveChangesAsync();
    }
    
}