using BasicSupermarket.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarketTests.Controllers;

public class TestControllerTests
{
    private readonly TestController _controller;

    public TestControllerTests()
    {
        _controller = new TestController();
    }

    [Fact]
    public void HealthCheck_ReturnsOkResult()
    {
        // Act
        var result = _controller.HealthCheck();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        dynamic returnValue = okResult.Value;
        Assert.Equal("API is running", returnValue.status);
    }
} 