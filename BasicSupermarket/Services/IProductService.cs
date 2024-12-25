using BasicSupermarket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Services;

public interface IProductService
{
    public Task<ActionResult<IEnumerable<Product>>> GetProducts();
    public Task<ActionResult<Product>> GetProduct(int id);
    public Task<ActionResult<Product>> PostProduct(Product product);
    public Task<IActionResult> PutProduct(int id, Product product);
    public Task<IActionResult> DeleteProduct(int id);
}