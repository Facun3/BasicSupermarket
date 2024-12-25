using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    private ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<int> AddAsync(Product product)
    {
        _context.Products.Add(product);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        return await _context.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(int id)
    {
        _context.Products.Remove(new Product() { Id = id });
        return _context.SaveChangesAsync();
    }
}