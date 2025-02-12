using System.Linq.Expressions;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Mapping;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Repositories;
using BasicSupermarket.Services;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable;
using MockQueryable.Moq;
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
        var queryableProducts = new List<Product>
        {
            new Product { 
                Id = 1, 
                Name = "Apple", 
                Description = "Fresh apple", 
                Price = 2.5m, 
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Fruits" } 
            },
            new Product { 
                Id = 2, 
                Name = "Banana", 
                Description = "Yellow banana", 
                Price = 1.2m, 
                CategoryId = 1,
                Category = new Category { Id = 1, Name = "Fruits" } 
            },
            new Product { 
                Id = 3, 
                Name = "Bread", 
                Description = "Whole grain bread", 
                Price = 3.0m, 
                CategoryId = 2,
                Category = new Category { Id = 2, Name = "Bakery" } 
            },
        }.AsQueryable();
        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(queryableProducts.BuildMock());

        var query = new ProductQuery
        {
            SearchFor = "Fruits",
            Page = 1,
            PageSize = 2,
            CategoryId = 1
        };

        // Act
        var result = await _productService.ListAsync(query);

        // Assert
        Assert.NotEmpty(result.Result); // Verify that there are products
        Assert.Equal(2, result.Total); // Both Apple and Banana should be returned
        Assert.Contains(result.Result, p => p.Name == "Apple");
        Assert.Contains(result.Result, p => p.Name == "Banana");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProduct_WhenExists()
    {
        // Arrange
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Description = "Description A" }
        };
        var queryableProducts = products.AsQueryable();

        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(queryableProducts.BuildMock());
        
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
        var products = new List<Product>
        {
            new Product { Id = 2, Name = "Product B", Description = "Description B" }
        };
        var queryableProducts = products.AsQueryable();

        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(queryableProducts.BuildMock());

        
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
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(new List<Product>().AsQueryable().BuildMock());
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
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(new List<Product>().AsQueryable().BuildMock());
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
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Old Product", Description = "Old Description" }
        };
        var queryableProducts = products.AsQueryable();
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(queryableProducts.BuildMock());
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);
        
        var updateRequest = new UpdateProductRequestDto { Name = "Updated Product", Description = "Updated Description" };
        
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
        var products = new List<Product>
        {
            new Product { Id = 5, Name = "Old Product", Description = "Old Description" }
        };
        var queryableProducts = products.AsQueryable();
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(queryableProducts.BuildMock());
        
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
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product to Delete" }
        };
        var queryableProducts = products.AsQueryable();
        
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(queryableProducts.BuildMock());
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
        var products = new List<Product>
        {
            new Product { Id = 5, Name = "Product that should be another than the product to be deleted" }
        };
        var queryableProducts = products.AsQueryable();
        
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(queryableProducts.BuildMock());
        _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()));
        // Act
        var response = await _productService.DeleteAsync(1);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }
}