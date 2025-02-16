using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class ProductController(IProductService productService): BaseApiController
{
    
    [HttpGet]
    [ProducesResponseType(typeof(QueryResponseDto<ProductResponseDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<QueryResponseDto<ProductResponseDto>>> ListAsync(
        [FromQuery] string name = "",
        [FromQuery] int? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            
            // Trim search term to prevent issues with spaces
            string trimmedSearchFor = string.IsNullOrWhiteSpace(name) ? "" : name.Trim();
            
            // Call to the service to get the results
            var results = await productService.ListAsync(new ProductQuery
            {
                SearchFor = trimmedSearchFor,
                CategoryId = category,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Page = page,
                PageSize = pageSize
            });

            return Ok(results); // Return 200 with the list of products
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logger here)
            // _logger.LogError(ex, "An error occurred while fetching products.");

            // Return 500 Internal Server Error with a generic message
            return StatusCode(500, "An unexpected error occurred while processing your request.");
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<ActionResult<ProductResponseDto>> GetByIdAsync(int id)
    {
        var result = await productService.GetByIdAsync(id);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDto), 201)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateProductRequestDto productRequestDto)
    {
        var result = await productService.CreateAsync(productRequestDto);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 201)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequestDto productRequestDto)
    {
        var result = await productService.UpdateAsync(id, productRequestDto);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ProductResponseDto), 200)]
    [ProducesResponseType(typeof(ErrorResponseDto), 400)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await productService.DeleteAsync(id);
        if (!result.Success) return BadRequest(new ErrorResponseDto(result.Message!));
        return Ok(result);
    }
}