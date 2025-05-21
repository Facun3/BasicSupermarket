using AutoMapper;
using BasicSupermarket.Domain.Dto.Cart;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;

namespace BasicSupermarket.Services;

public class CartService(ICartRepository cartRepository, IUnitOfWork unitOfWork,IMapper mapper): ICartService
{
    public async Task<Response<CartResponseDto>> GetCartByUserIdAsync(string userId)
    {
        var cartExist = await cartRepository.GetCartByUserIdAsync(userId);
        if (cartExist == null) 
            return Response<CartResponseDto>.FailureResponse("Cart not found");
        return Response<CartResponseDto>.SuccessResponse(mapper.Map<CartResponseDto>(cartExist));
    }

    public async Task<Response<CartResponseDto>> AddProductToCartAsync(string userId, AddCartItemRequestDto cartItemDto)
    {
        Cart cart = await cartRepository.GetCartByUserIdAsync(userId) ?? Cart.Create(userId);
        var newCartItem = CartItem.Create(cart.Id, cartItemDto.ProductId, cartItemDto.Quantity);
        cart.AddItem(newCartItem);
        if (cart.Id == 0) 
            await cartRepository.AddAsync(cart);
        else 
            cartRepository.Update(cart);

        await unitOfWork.CompleteAsync();

        return Response<CartResponseDto>.SuccessResponse(mapper.Map<CartResponseDto>(cart));
    }

    public async Task<Response<CartResponseDto>> RemoveProductFromCartAsync(string userId, int productId)
    {
        Cart? cart = await cartRepository.GetCartByUserIdAsync(userId);
        if(cart == null)
            return Response<CartResponseDto>.FailureResponse("Cart not found");
        cart.RemoveItem(productId);
        cartRepository.Update(cart);
        await unitOfWork.CompleteAsync();
        return Response<CartResponseDto>.SuccessResponse(mapper.Map<CartResponseDto>(cart));
        
    }

    public async Task ClearCartAsync(string userId)
    {
        var cart = await cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return;
        cart.Clear();
        await unitOfWork.CompleteAsync();
    }
}
