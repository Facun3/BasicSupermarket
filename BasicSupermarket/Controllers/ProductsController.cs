using BasicSupermarket.Domain.Dtos;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Persistence;
using BasicSupermarket.Repositories;
using BasicSupermarket.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController: ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetProductsAsync(string? name = null, int page = 1, int pageSize = 10)
    {
        try
        {
            var results = await _productService.GetProducts(name, page, pageSize);
            return Ok(results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(int id)
    {
        var product = await _productService.GetProduct(id);
        if (product == null) return NotFound();
        return product;
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> PostProduct([FromBody] CreateProductRequestDto productRequestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var newProduct = await _productService.PostProduct(productRequestDto);
        
        return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(UpdateProductRequestDto productRequestDto)
    {
        try
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(!ProductExists(productRequestDto.Id)) return NotFound();
            return Ok(await _productService.PutProduct(productRequestDto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        if (_productService.GetProduct(id).Result == null)
        {
            return NotFound();
        }

        try
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    private bool ProductExists(int id)
    {
        return _productService.GetProduct(id).Result == null;
    }
}