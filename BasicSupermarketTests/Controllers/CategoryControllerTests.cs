using BasicSupermarket.Controllers;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Dto.Category;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasicSupermarketTests.Controllers;

public class CategoryControllerTests : TestBase
{
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _controller = new CategoryController(CategoryServiceMock.Object);
    }

    [Fact]
    public async Task GetCategories_ReturnsOkResult()
    {
        // Arrange
        var expectedCategories = new List<CategoryResponseDto>
        {
            new() { Id = 1, Name = "Test Category" }
        };

        CategoryServiceMock.Setup(x => x.ListAsync())
            .ReturnsAsync(expectedCategories);

        // Act
        var result = await _controller.GetCategories();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<CategoryResponseDto>>(okResult.Value);
        Assert.Equal(expectedCategories, returnValue);
    }

    [Fact]
    public async Task PostCategory_WithValidCategory_ReturnsOkResult()
    {
        // Arrange
        var createRequest = new CreateCategoryRequestDto
        {
            Name = "New Category",
            Description = "Test Description"
        };

        var expectedResponse = new Response<CategoryResponseDto>(
            new CategoryResponseDto
            {
                Id = 1,
                Name = createRequest.Name,
                Description = createRequest.Description
            });

        CategoryServiceMock.Setup(x => x.SaveAsync(It.IsAny<CreateCategoryRequestDto>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostCategory(createRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<CategoryResponseDto>(okResult.Value);
        Assert.Equal(createRequest.Name, returnValue.Name);
    }

    [Fact]
    public async Task PostCategory_WithDuplicateName_ReturnsBadRequest()
    {
        // Arrange
        var createRequest = new CreateCategoryRequestDto
        {
            Name = "Existing Category",
            Description = "Test Description"
        };

        var expectedResponse = new Response<CategoryResponseDto>("Category Name Already Exists");

        CategoryServiceMock.Setup(x => x.SaveAsync(It.IsAny<CreateCategoryRequestDto>()))
            .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.PostCategory(createRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var returnValue = Assert.IsType<ErrorResponseDto>(badRequestResult.Value);
        Assert.Equal("Category Name Already Exists", returnValue.Message);
    }
} 