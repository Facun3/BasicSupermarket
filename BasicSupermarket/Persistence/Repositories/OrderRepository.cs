using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Persistence.Repositories;

public class OrderRepository: BaseRepository<Order>, IOrderRepository
{
    private readonly AppDbContext  _context;

    public OrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}