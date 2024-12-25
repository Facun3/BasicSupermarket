using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<int> AddAsync(Product product);
    Task<int> UpdateAsync(Product product);
    Task<int> DeleteAsync(int id);
}