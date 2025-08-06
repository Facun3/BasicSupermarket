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
    private readonly Mock<IConfiguration> _configurationMock;

    public AuthControllerTests()
    {
        _configurationMock = new Mock<IConfiguration>();
        _configurationMock.Setup(x => x.GetSection("JwtSettings"))
            .Returns(new Mock<IConfigurationSection>().Object);
        _configurationMock.Setup(x => x.GetSection("JwtSettings:Secret"))
            .Returns(new Mock<IConfigurationSection>().Object);
        _configurationMock.Setup(x => x.GetSection("JwtSettings:Issuer"))
            .Returns(new Mock<IConfigurationSection>().Object);
        _configurationMock.Setup(x => x.GetSection("JwtSettings:Audience"))
            .Returns(new Mock<IConfigurationSection>().Object);
        _configurationMock.Setup(x => x.GetSection("JwtSettings:ExpirationMinutes"))
            .Returns(new Mock<IConfigurationSection>().Object);

        _controller = new AuthController(UserManagerMock.Object, _configurationMock.Object);
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
        var returnValue = Assert.IsType<dynamic>(okResult.Value);
        Assert.NotNull(returnValue.token);
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
        var returnValue = Assert.IsType<dynamic>(unauthorizedResult.Value);
        Assert.Equal("Invalid credentials", returnValue.message);
    }
} 