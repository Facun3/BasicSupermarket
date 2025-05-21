using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Persistence.Context;

namespace BasicSupermarket.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context): base(context) { }
}