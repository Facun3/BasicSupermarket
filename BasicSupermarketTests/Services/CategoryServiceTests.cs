using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Repositories;
using BasicSupermarket.Services;
using Microsoft.Extensions.Logging.Abstractions;
using MockQueryable;
using Moq;

namespace BasicSupermarketTests.Services;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _categoryService = new CategoryService(
            _categoryRepositoryMock.Object,
            _unitOfWorkMock.Object,
            new NullLogger<CategoryService>() // Logger "vacío" para simplificar pruebas
        );
    }

    [Fact]
    public async Task ListAsync_ShouldReturnAllCategories()
    {
        // Arrange
        var categories = new List<Category>
        {
            new Category { Id = 1, Name = "Category 1" },
            new Category { Id = 2, Name = "Category 2" }
        };
        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(categories.AsQueryable().BuildMock());

        // Act
        var result = await _categoryService.ListAsync();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.Contains(result, c => c.Name == "Category 1");
        Assert.Contains(result, c => c.Name == "Category 2");
    }

    [Fact]
    public async Task SaveAsync_ShouldSaveCategory_WhenNoException()
    {
        // Arrange
        var newCategory = new Category { Id = 1, Name = "New Category" };
        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(new List<Category>().AsQueryable().BuildMock());
        _categoryRepositoryMock.Setup(repo => repo.AddAsync(newCategory)).Returns(Task.CompletedTask);

        // Act
        var response = await _categoryService.SaveAsync(newCategory);

        // Assert
        Assert.True(response.Success);
        Assert.Equal(newCategory, response.Resource);
        _categoryRepositoryMock.Verify(repo => repo.AddAsync(newCategory), Times.Once);
    }

    [Fact]
    public async Task SaveAsync_ShouldReturnError_WhenExceptionOccurs()
    {
        // Arrange
        var newCategory = new Category { Id = 1, Name = "New Category" };
        _categoryRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Category>()))
                               .ThrowsAsync(new Exception("Database error"));

        // Act
        var response = await _categoryService.SaveAsync(newCategory);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("An error occurred", response.Message);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateCategory_WhenExists()
    {
        // Arrange
        var existingCategory = new Category { Id = 1, Name = "Old Category" };
        var categories = new List<Category> { existingCategory };
        var updatedCategory = new Category { Name = "Updated Category" };

        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(categories.AsQueryable().BuildMock());
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _categoryService.UpdateAsync(1, updatedCategory);

        // Assert
        Assert.True(response.Success);
        Assert.Equal("Updated Category", existingCategory.Name);
        _categoryRepositoryMock.Verify(repo => repo.Update(existingCategory), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnError_WhenCategoryDoesNotExist()
    {
        // Arrange
        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(new List<Category>().AsQueryable().BuildMock());

        // Act
        var response = await _categoryService.UpdateAsync(1, new Category { Name = "Updated Category" });

        // Assert
        Assert.False(response.Success);
        Assert.Contains("does not exist", response.Message);
    }

    [Fact]
    public async Task DeleteAsync_ShouldDeleteCategory_WhenExists()
    {
        // Arrange
        var existingCategory = new Category { Id = 1, Name = "Old Category" };
        var categories = new List<Category> { existingCategory };
        IQueryable<Category> query = categories.AsQueryable();
        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(query.BuildMock());
        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var response = await _categoryService.DeleteAsync(1);

        // Assert
        Assert.True(response.Success);
        Assert.Equal(existingCategory, response.Resource);
        _categoryRepositoryMock.Verify(repo => repo.Delete(existingCategory), Times.Once);
        _unitOfWorkMock.Verify(uow => uow.CompleteAsync(), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnError_WhenCategoryDoesNotExist()
    {
        var categories = new List<Category>().AsQueryable().BuildMock();
        // Arrange
        _categoryRepositoryMock.Setup(repo => repo.GetQuery()).Returns(categories);

        // Act
        var response = await _categoryService.DeleteAsync(1);

        // Assert
        Assert.False(response.Success);
        Assert.Contains("does not exist", response.Message);
    }
}