using BasicSupermarket.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BasicSupermarketTests;

public abstract class TestBase
{
    protected readonly Mock<IProductService> ProductServiceMock;
    protected readonly Mock<ICategoryService> CategoryServiceMock;
    protected readonly Mock<ICartService> CartServiceMock;
    protected readonly Mock<UserManager<IdentityUser>> UserManagerMock;
    
    protected TestBase()
    {
        ProductServiceMock = new Mock<IProductService>();
        CategoryServiceMock = new Mock<ICategoryService>();
        CartServiceMock = new Mock<ICartService>();
        
        var store = new Mock<IUserStore<IdentityUser>>();
        UserManagerMock = new Mock<UserManager<IdentityUser>>(
            store.Object, null, null, null, null, null, null, null, null);
    }
} 