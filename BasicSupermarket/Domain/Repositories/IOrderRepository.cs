using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Domain.Repositories;

public interface IOrderRepository: IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
}