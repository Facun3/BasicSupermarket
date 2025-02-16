using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(string userId);
}