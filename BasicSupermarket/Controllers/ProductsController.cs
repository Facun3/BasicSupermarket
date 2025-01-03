using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class ProductsController: BaseApiController
{
    private readonly IProductService _productService;
    
    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> ListAsync(string? name = null, int page = 1, int pageSize = 10)
    {
        try
        {
            var results = await _productService.ListAsync(new ProductQuery{SearchFor = name, Page = page, PageSize = pageSize });
            return Ok(results);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<ActionResult<ProductResponseDto>> GetByIdAsync(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), 201)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequestDto productRequestDto)
    {
        var result = await _productService.CreateAsync(productRequestDto);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 201)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> Update(int id, [FromBody ]UpdateProductRequestDto productRequestDto)
    {
        var result = await _productService.UpdateAsync(id, productRequestDto);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
}