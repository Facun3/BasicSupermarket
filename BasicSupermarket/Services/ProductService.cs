using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Services;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductService> _logger;


    private ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }
    
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        try
        {
            IEnumerable<Product> products = await _productRepository.GetAllAsync();
            return products.ToList();
        }

        catch (Exception ex)
        {
            String err = ex.Message;
            _logger.LogError(ex, ex.Message);
            throw new ApplicationException(err);
        }
    }

    public Task<ActionResult<Product>> GetProduct(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ActionResult<Product>> PostProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> PutProduct(int id, Product product)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}