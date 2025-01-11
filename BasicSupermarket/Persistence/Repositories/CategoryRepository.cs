using System.Linq.Expressions;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Persistence.Context;
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

    public IQueryable<Category> GetQuery()
    {
        return _context.Categories.AsQueryable();
    }
    
    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
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