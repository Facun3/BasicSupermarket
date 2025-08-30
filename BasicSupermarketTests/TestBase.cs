using BasicSupermarket.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Microsoft.Extensions.Configuration;

namespace BasicSupermarketTests;

public abstract class TestBase
{
    protected readonly Mock<IProductService> ProductServiceMock;
    protected readonly Mock<ICategoryService> CategoryServiceMock;
    protected readonly Mock<ICartService> CartServiceMock;
    protected readonly Mock<UserManager<IdentityUser>> UserManagerMock;
    protected readonly Mock<IConfiguration> ConfigurationMock;

    protected TestBase()
    {
        ProductServiceMock = new Mock<IProductService>();
        CategoryServiceMock = new Mock<ICategoryService>();
        CartServiceMock = new Mock<ICartService>();
        ConfigurationMock = SetupConfigurationMock();
        UserManagerMock = SetupUserManagerMock();
    }

    private Mock<UserManager<IdentityUser>> SetupUserManagerMock()
    {
        var store = new Mock<IUserStore<IdentityUser>>();
        var options = new Mock<IOptions<IdentityOptions>>();
        var userValidators = new List<IUserValidator<IdentityUser>>();
        var passwordValidators = new List<IPasswordValidator<IdentityUser>>();
        var keyNormalizer = new UpperInvariantLookupNormalizer();
        var errors = new IdentityErrorDescriber();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<IdentityUser>>>();

        var userManager = new Mock<UserManager<IdentityUser>>(
            store.Object,
            options.Object,
            new PasswordHasher<IdentityUser>(),
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services.Object,
            logger.Object);

        var defaultUser = new IdentityUser
        {
            Id = "1",
            Email = "test@example.com",
            UserName = "test@example.com"
        };

        userManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(defaultUser);

        userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        userManager.Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>()))
            .ReturnsAsync(new List<string> { "User" });

        return userManager;
    }

    private Mock<IConfiguration> SetupConfigurationMock()
    {
        var configMock = new Mock<IConfiguration>();
        
        // JWT Settings section
        var jwtSection = new Mock<IConfigurationSection>();
        configMock.Setup(x => x.GetSection("JwtSettings")).Returns(jwtSection.Object);

        // La clave debe tener al menos 256 bits (32 bytes) para HS256
        var secretKey = "your-256-bit-secret-your-256-bit-secret-your-256-bit-secret";
        var secretSection = new Mock<IConfigurationSection>();
        secretSection.Setup(x => x.Value).Returns(secretKey);
        jwtSection.Setup(x => x["Secret"]).Returns(secretKey);

        var issuerSection = new Mock<IConfigurationSection>();
        issuerSection.Setup(x => x.Value).Returns("BasicSupermarket");
        jwtSection.Setup(x => x["Issuer"]).Returns("BasicSupermarket");

        var audienceSection = new Mock<IConfigurationSection>();
        audienceSection.Setup(x => x.Value).Returns("BasicSupermarketUsers");
        jwtSection.Setup(x => x["Audience"]).Returns("BasicSupermarketUsers");

        var expirationSection = new Mock<IConfigurationSection>();
        expirationSection.Setup(x => x.Value).Returns("60");
        jwtSection.Setup(x => x["ExpirationMinutes"]).Returns("60");

        return configMock;
    }
}
