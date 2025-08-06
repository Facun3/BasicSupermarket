using BasicSupermarket.Domain.Dto.Cart;
using BasicSupermarket.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class CartController : BaseApiController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.GetCartByUserIdAsync(userId);
        return result.Success ? Ok(result) : NotFound(result.Message);
    }

    [HttpPost("items")]
    [Authorize]
    public async Task<IActionResult> AddItem([FromBody] AddCartItemRequestDto request)
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.AddProductToCartAsync(userId, request);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpDelete("items/{productId:int}")]
    [Authorize]
    public async Task<IActionResult> RemoveItem(int productId)
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        var result = await _cartService.RemoveProductFromCartAsync(userId, productId);
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    [HttpDelete("clear")]
    [Authorize]
    public async Task<IActionResult> ClearCart()
    {
        var userId = User.Identity?.Name;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _cartService.ClearCartAsync(userId);
        return Ok();
    }
}
