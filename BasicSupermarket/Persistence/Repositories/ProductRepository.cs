using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence.Context;
using BasicSupermarket.Domain.Repositories;

namespace BasicSupermarket.Persistence.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<Product> GetQuery()
    {
        return _context.Products.AsQueryable();
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