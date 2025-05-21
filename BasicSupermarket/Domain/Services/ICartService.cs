using BasicSupermarket.Domain.Dto.Cart;
using BasicSupermarket.Domain.Services.Communication;

namespace BasicSupermarket.Domain.Services;

public interface ICartService
{
    Task<Response<CartResponseDto>> GetCartByUserIdAsync(string userId);
    Task<Response<CartResponseDto>> AddProductToCartAsync(string userId, AddCartItemRequestDto cartItemDto);
    Task<Response<CartResponseDto>> RemoveProductFromCartAsync(string userId, int productId);
    Task ClearCartAsync(string userId);
}