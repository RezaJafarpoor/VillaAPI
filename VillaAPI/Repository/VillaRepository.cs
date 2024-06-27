using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VillaAPI.Data;
using VillaAPI.DTOs;
using VillaAPI.Entities;
using VillaAPI.Repository.IRepository;

namespace VillaAPI.Repository;

public class VillaRepository : GenericRepository<Villa>, IVillaRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VillaRepository( ApplicationDbContext dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Villa> UpdateAsync(Villa entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        _dbContext.Villas.Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }


    

   
}