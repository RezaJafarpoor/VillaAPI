using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.DTOs;
using VillaAPI.Entities;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository;

public class VillaRepository : IVillaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VillaRepository( ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Create(Villa entity)
    {
       await _dbContext.Villas.AddAsync(entity);
       await Save();
    }

    public async Task Remove(Villa entity)
    {
         _dbContext.Villas.Remove(entity);
        await Save();
    }

    public async Task<List<Villa>> GetAll(Expression<Func<Villa,bool>>? filter = null)
    {
        IQueryable<Villa> query = _dbContext.Villas;
        if (filter != null)
        {
            query = query.Where(filter);

        }

        return await query.ToListAsync();
    }

    public async Task<Villa> Get(Expression<Func<Villa,bool>>? filter = null, bool tracked = true)
    {
        IQueryable<Villa> query = _dbContext.Villas;
        if (!tracked)
        {
            query.AsNoTracking();
        }

        if (filter !=null)
        {
            query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task Save()
    {
       await _dbContext.SaveChangesAsync();
    }
}