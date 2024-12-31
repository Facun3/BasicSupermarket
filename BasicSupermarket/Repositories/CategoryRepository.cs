using System.Linq.Expressions;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<IQueryable<Category>> SearchAsync(Expression<Func<Category, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Category> AddAsync(Category entity)
    {
        var newEntity = _context.Categories.Add(entity).Entity;
        return Task.FromResult(newEntity);
    }

    public Task<Category?> GetByIdAsync(int id)
    {
        return _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public Task<Category> UpdateAsync(Category entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(int id)
    {
        _context.Categories.Remove(new Category { Id = id });
        return _context.SaveChangesAsync();
    }
}