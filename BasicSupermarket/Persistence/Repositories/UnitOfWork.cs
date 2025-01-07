using BasicSupermarket.Persistence.Context;
using BasicSupermarket.Repositories;

namespace BasicSupermarket.Persistence.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public Task CompleteAsync()
    {
        return _context.SaveChangesAsync();
    }
}