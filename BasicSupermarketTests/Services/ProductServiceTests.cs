using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto.Product;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Services;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable;
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
            Product.Create("Apple", "Fresh apple", 2.5m, 1, 1),
            Product.Create("Banana", "Yellow banana", 1.2m, 1, 1),
            Product.Create("Bread", "Whole grain bread", 3.0m, 1, 2)
        };
        Category fruitsCategory = new Category
        {
            Id = 1,
            Name = "Fruits"
        };
        Category othersCategory = new Category
        {
            Id = 2,
            Name = "Others"
        };
        
        products[0].SetCategory(fruitsCategory);
        products[1].SetCategory(fruitsCategory);
        products[2].SetCategory(othersCategory);
        
        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(products.AsQueryable().BuildMock());

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
        var product1 = Product.Create("Product A", "Description A", 1000, 0, 0);
        product1.SetId(1);
        var products = new List<Product> 
        {
            product1
        };
        
        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(products.AsQueryable().BuildMock());
        
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
        var product1 = Product.Create("Product A", "Description A", 1000, 0, 0);
        product1.SetId(2);
        var products = new List<Product> 
        {
            product1
        };

        _productRepositoryMock
            .Setup(repo => repo.GetQuery())
            .Returns(products.AsQueryable().BuildMock());

        
        // Act
        var response = await _productService.GetByIdAsync(1);

        // Assert
        Assert.False(response.Success);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateProduct_WhenValid()
    {
        // Arrange
        var productRequest = new CreateProductRequestDto { Name = "New Product", Description = "Description", Price = 25000, Quantity = 0, CategoryId = 0 };
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
        var productRequest = new CreateProductRequestDto { Name = "New Product", Description = "Description", Quantity = 0, CategoryId = 0 ,Price = 25000 };
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(new List<Product>().AsQueryable().BuildMock());
        _productRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                              .ThrowsAsync(new Exception("Database error"));

        // Act
        var response = await _productService.CreateAsync(productRequest);

        // Assert
        Assert.False(response.Success);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProduct_WhenExists()
    {
        // Arrange
        var product1 = Product.Create("Product A", "Description A", 1000, 0, 0);
        product1.SetId(1);
        var products = new List<Product>
        {
            product1
        };
        var queryableProducts = products.AsQueryable();
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(queryableProducts.BuildMock());
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);
        
        var updateRequest = new UpdateProductRequestDto { Name = "Updated Product", Description = "Updated Description", Price = 2000};
        
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
        var product1 = Product.Create("Product A", "Description A", 1000, 0, 0);
        product1.SetId(0);
        var products = new List<Product>
        {
            product1
        };
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(products.AsQueryable().BuildMock());
        
        // Act
        var response = await _productService.UpdateAsync(1, new UpdateProductRequestDto { Name = "Name", Description = "Description", ImageUrl = "www.google.com/image.jpg", Price = 10000});

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteProduct_WhenExists()
    {
        // Arrange
        var product1 = Product.Create("Product to Delete", "Description A", 1000, 0, 0);
        product1.SetId(1);
        var products = new List<Product>
        {
            product1
        };
        
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(products.AsQueryable().BuildMock());
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
        var product5  = Product.Create("Product that should be another than the product to be deleted", "Description A", 1000, 0, 0);
        product5.SetId(5);
        var products = new List<Product>
        {
            product5
        };
        _productRepositoryMock.Setup(repo => repo.GetQuery()).Returns(products.AsQueryable().BuildMock());
        _productRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Product>()));
        // Act
        var response = await _productService.DeleteAsync(1);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("Product not found", response.Message);
    }
}