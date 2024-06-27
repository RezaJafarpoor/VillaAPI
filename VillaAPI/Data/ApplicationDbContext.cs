using Microsoft.EntityFrameworkCore;
using VillaAPI.Entities;

namespace VillaAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Villa> Villas { get; set; }
}