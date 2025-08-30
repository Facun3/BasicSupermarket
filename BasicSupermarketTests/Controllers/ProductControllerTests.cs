using BasicSupermarket.Controllers;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Dto.Product;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;
using BasicSupermarket.Domain.Communication;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasicSupermarketTests.Controllers;

public class ProductControllerTests : TestBase
{
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _controller = new ProductController(ProductServiceMock.Object);
    }

    [Fact]
    public async Task ListAsync_WithValidParameters_ReturnsOkResult()
    {
        // Arrange
        var expectedResponse = new QueryResponseDto<ProductResponseDto>
        {
            Page = 1,
            PageSize = 10,
            Total = 1,
            Result = new List<ProductResponseDto>
            {
                new() { Id = 1, Name = "Test Product" }
            }
        };

        ProductServiceMock.Setup(x => x.ListAsync(It.IsAny<ProductQuery>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.ListAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<QueryResponseDto<ProductResponseDto>>(okResult.Value);
        Assert.Equal(expectedResponse, returnValue);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var productId = 1;
        var expectedResponse = new Response<ProductResponseDto>(
            new ProductResponseDto { Id = productId, Name = "Test Product" });

        ProductServiceMock.Setup(x => x.GetByIdAsync(productId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetByIdAsync(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(productId, returnValue.Id);
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        var productId = 999;
        var expectedResponse = new Response<ProductResponseDto>("Product not found");

        ProductServiceMock.Setup(x => x.GetByIdAsync(productId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.GetByIdAsync(productId);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var returnValue = Assert.IsType<ErrorResponseDto>(badRequestResult.Value);
        Assert.Equal("Product not found", returnValue.Message);
    }

    [Fact]
    public async Task CreateAsync_WithValidProduct_ReturnsOkResult()
    {
        // Arrange
        var createRequest = new CreateProductRequestDto
        {
            Name = "New Product",
            Description = "Test Description",
            Price = 10.99m,
            Quantity = 100,
            CategoryId = 1
        };

        var expectedResponse = new Response<ProductResponseDto>(
            new ProductResponseDto
            {
                Id = 1,
                Name = createRequest.Name,
                Description = createRequest.Description,
                Price = createRequest.Price,
                CategoryId = createRequest.CategoryId
            });

        ProductServiceMock.Setup(x => x.CreateAsync(It.IsAny<CreateProductRequestDto>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.CreateAsync(createRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(createRequest.Name, returnValue.Name);
    }

    [Fact]
    public async Task UpdateAsync_WithValidProduct_ReturnsOkResult()
    {
        // Arrange
        var productId = 1;
        var updateRequest = new UpdateProductRequestDto
        {
            Name = "Updated Product",
            Description = "Updated Description",
            Price = 15.99m,
            ImageUrl = "http://example.com/image.jpg",
            CategoryId = 2
        };

        var expectedResponse = new Response<ProductResponseDto>(
            new ProductResponseDto
            {
                Id = productId,
                Name = updateRequest.Name,
                Description = updateRequest.Description,
                Price = updateRequest.Price,
                ImageUrl = updateRequest.ImageUrl,
                CategoryId = updateRequest.CategoryId.Value
            });

        ProductServiceMock.Setup(x => x.UpdateAsync(productId, It.IsAny<UpdateProductRequestDto>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Update(productId, updateRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(updateRequest.Name, returnValue.Name);
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ReturnsOkResult()
    {
        // Arrange
        var productId = 1;
        var expectedResponse = new Response<ProductResponseDto>(
            new ProductResponseDto { Id = productId, Name = "Deleted Product" });

        ProductServiceMock.Setup(x => x.DeleteAsync(productId))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ProductResponseDto>(okResult.Value);
        Assert.Equal(productId, returnValue.Id);
    }
} 