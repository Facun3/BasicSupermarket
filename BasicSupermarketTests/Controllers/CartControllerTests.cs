using BasicSupermarket.Controllers;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Dto.Cart;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasicSupermarketTests.Controllers;

public class CartControllerTests : TestBase
{
    private readonly CartController _controller;

    public CartControllerTests()
    {
        _controller = new CartController(CartServiceMock.Object);
    }

    [Fact]
    public async Task GetCart_WithValidUserId_ReturnsOkResult()
    {
        // Arrange
        var userId = "user123";
        var expectedCart = new CartResponseDto
        {
            Id = 1,
            UserId = userId,
            Items = new List<CartItemResponseDto>(),
            TotalPrice = 0
        };

        var expectedResponse = new Response<CartResponseDto>(expectedCart);

        CartServiceMock.Setup(x => x.GetCartByUserIdAsync(userId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetCart();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartResponseDto>(okResult.Value);
        Assert.Equal(userId, returnValue.UserId);
    }

    [Fact]
    public async Task AddToCart_WithValidItem_ReturnsOkResult()
    {
        // Arrange
        var userId = "user123";
        var addItemRequest = new AddCartItemRequestDto
        {
            ProductId = 1,
            Quantity = 2
        };

        var expectedCart = new CartResponseDto
        {
            Id = 1,
            UserId = userId,
            Items = new List<CartItemResponseDto>
            {
                new()
                {
                    ProductId = addItemRequest.ProductId,
                    Quantity = addItemRequest.Quantity,
                    UnitPrice = 10.99m,
                    Subtotal = addItemRequest.Quantity * 10.99m
                }
            },
            TotalPrice = addItemRequest.Quantity * 10.99m
        };

        var expectedResponse = new Response<CartResponseDto>(expectedCart);

        CartServiceMock.Setup(x => x.AddProductToCartAsync(userId, addItemRequest))
            .ReturnsAsync(expectedResponse);
        
        // Act
        var result = await _controller.AddItem(addItemRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartResponseDto>(okResult.Value);
        Assert.Equal(userId, returnValue.UserId);
        Assert.Single(returnValue.Items);
    }

    [Fact]
    public async Task RemoveFromCart_WithValidProductId_ReturnsOkResult()
    {
        // Arrange
        var userId = "user123";
        var productId = 1;

        var expectedCart = new CartResponseDto
        {
            Id = 1,
            UserId = userId,
            Items = new List<CartItemResponseDto>(),
            TotalPrice = 0
        };

        var expectedResponse = new Response<CartResponseDto>(expectedCart);

        CartServiceMock.Setup(x => x.RemoveProductFromCartAsync(userId, productId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.RemoveItem(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CartResponseDto>(okResult.Value);
        Assert.Equal(userId, returnValue.UserId);
        Assert.Empty(returnValue.Items);
    }

    [Fact]
    public async Task ClearCart_WithValidUserId_ReturnsOkResult()
    {
        // Arrange
        var userId = "user123";

        // Act
        var result = await _controller.ClearCart();

        // Assert
        Assert.IsType<OkResult>(result);
        CartServiceMock.Verify(x => x.ClearCartAsync(userId), Times.Once);
    }
} 