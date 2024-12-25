using BasicSupermarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Persistence;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    // DbSet properties
    public DbSet<Product> Products { get; set; }
}