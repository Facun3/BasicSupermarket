using BasicSupermarket.Controllers;
using BasicSupermarket.Domain.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BasicSupermarketTests.Controllers;

public class AuthControllerTests : TestBase
{
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _controller = new AuthController(UserManagerMock.Object, ConfigurationMock.Object);
    }

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsOkResult()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        var user = new IdentityUser
        {
            Id = "1",
            Email = loginRequest.Email,
            UserName = loginRequest.Email
        };

        UserManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
            .ReturnsAsync(user);

        UserManagerMock.Setup(x => x.CheckPasswordAsync(user, loginRequest.Password))
            .ReturnsAsync(true);

        UserManagerMock.Setup(x => x.GetRolesAsync(user))
            .ReturnsAsync(new List<string> { "User" });

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
        var token = okResult.Value.GetType().GetProperty("token").GetValue(okResult.Value, null);
        Assert.NotNull(token);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
    {
        // Arrange
        var loginRequest = new LoginRequest
        {
            Email = "test@example.com",
            Password = "WrongPassword"
        };

        UserManagerMock.Setup(x => x.FindByEmailAsync(loginRequest.Email))
            .ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _controller.Login(loginRequest);

        // Assert
        var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
        Assert.NotNull(unauthorizedResult.Value);
        Assert.Equal("Invalid credentials", unauthorizedResult.Value.GetType().GetProperty("message").GetValue(unauthorizedResult.Value, null));
    }
}
