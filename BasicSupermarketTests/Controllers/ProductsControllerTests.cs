using BasicSupermarket.Controllers;
using BasicSupermarket.Services;

namespace BasicSupermarketTests.Controllers;

public class ProductsControllerTests
{
    private readonly ProductsController _controller;
    private readonly IProductService _productService;
    
    public ProductsControllerTests()
    {
        _controller = new ProductsController(_productService);
    }
    [Fact]
    public void GetByIdNotFound()
    {
        var id = 11;
        var result = _controller.GetProduct(id);
        
    }
}