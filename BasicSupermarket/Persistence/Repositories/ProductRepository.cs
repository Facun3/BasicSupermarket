using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence.Context;
using BasicSupermarket.Domain.Repositories;

namespace BasicSupermarket.Persistence.Repositories;

public class ProductRepository: BaseRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }
}