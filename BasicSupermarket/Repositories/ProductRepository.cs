using System.Linq.Expressions;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
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

    public Task<IQueryable<Product>> SearchAsync(Expression<Func<Product, bool>> predicate)
    {
        return Task.FromResult(_context.Products.Where(predicate));
    }

    public async Task<Product> AddAsync(Product product)
    {
        var category = await _context.Categories.FindAsync(product.CategoryId);
        if (category != null)
        {
            product.Category = category;
        }
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public Task<int> DeleteAsync(int id)
    {
        _context.Products.Remove(new Product { Id = id });
        return _context.SaveChangesAsync();
    }
}