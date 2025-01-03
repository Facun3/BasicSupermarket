using System.Linq.Expressions;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category>> SearchAsync(Expression<Func<Category, bool>> predicate)
    {
        return await _context.Categories.AsQueryable().Where(predicate).AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(Category entity)
    {
        await _context.Categories.AddAsync(entity);
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.AsNoTracking().ToListAsync();
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }

    public void Delete(Category category)
    {
        _context.Categories.Remove(category);
    }
}