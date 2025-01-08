using System.Linq.Expressions;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Mapping;
using BasicSupermarket.Repositories;
using BasicSupermarket.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BasicSupermarketTests.Services;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ProductService _productService;

    public ProductServiceTests()
    {
        _productRepositoryMock = new Mock<IProductRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _productService = new ProductService(
            _productRepositoryMock.Object,
            _unitOfWorkMock.Object,
            new NullLogger<ProductService>()
        );
    }

    [Fact]
    public async Task ListAsync_ShouldReturnFilteredAndPaginatedProducts()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Description = "Description A" },
            new Product { Id = 2, Name = "Product B", Description = "Description B" }
        };

        _productRepositoryMock
            .Setup(repo => repo.SearchAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(products);

        var query = new ProductQuery { SearchFor = "A", Page = 1, PageSize = 1 };

        // Act
        var result = await _productService.ListAsync(query);

        // Assert
        Assert.Single(result);
        Assert.Contains(result, p => p.Name == "Product A");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "Product A", Description = "Description A" };
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

        // Act
        var response = await _productService.GetByIdAsync(1);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Product A", response.Resource.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnError_WhenProductNotFound()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

        // Act
        var response = await _productService.GetByIdAsync(1);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProduct_WhenValid()
    {
        // Arrange
        var productRequest = new CreateProductRequestDto { Name = "New Product", Description = "Description" };
        var product = ProductMapper.FromCreateProductRequestDtoToProduct(productRequest);

        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _productService.CreateAsync(productRequest);

        // Assert
        Assert.True(response.Success);
        Assert.Equal(productRequest.Name, response.Resource.Name);
        _productRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnError_WhenExceptionOccurs()
    {
        // Arrange
        var productRequest = new CreateProductRequestDto { Name = "New Product", Description = "Description" };

        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                              .ThrowsAsync(new Exception("Database error"));

        // Act
        var response = await _productService.CreateAsync(productRequest);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Error creating product", response.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenExists()
    {
        // Arrange
        var existingProduct = new Product { Id = 1, Name = "Old Product", Description = "Old Description" };
        var updateRequest = new UpdateProductRequestDto { Name = "Updated Product", Description = "Updated Description" };

        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingProduct);
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _productService.UpdateAsync(1, updateRequest);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Updated Product", response.Resource.Name);
        _productRepositoryMock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenProductNotFound()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

        // Act
        var response = await _productService.UpdateAsync(1, new UpdateProductRequestDto { Name = "Name" });

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProduct_WhenExists()
    {
        // Arrange
        var existingProduct = new Product { Id = 1, Name = "Product to Delete" };
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingProduct);
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _productService.DeleteAsync(1);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Product to Delete", response.Resource.Name);
        _productRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnError_WhenProductNotFound()
    {
        // Arrange
        _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Product)null);

        // Act
        var response = await _productService.DeleteAsync(1);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }
}