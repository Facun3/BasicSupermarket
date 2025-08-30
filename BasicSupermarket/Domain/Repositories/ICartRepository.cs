using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Domain.Repositories;

public interface ICartRepository: IRepository<Cart>
{
    Task<Cart?> GetCartByUserIdAsync(string userId);
}