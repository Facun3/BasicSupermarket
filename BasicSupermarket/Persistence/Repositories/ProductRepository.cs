using System.Linq.Expressions;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Persistence.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Category).ToListAsync();
    }

    public async Task<IEnumerable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _context.Products.Include(p => p.Category).Where(predicate).ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _context.Products.Remove(product);
    }
}