using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class CategoryController(ICategoryService categoryService): BaseApiController
{
    
    [HttpGet("all")]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return Ok(await categoryService.ListAsync());
    }

    [HttpPost("")]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<Category>>> PostCategory([FromBody] CreateCategoryRequestDto category)
    {
        try
        {
            return Ok(await categoryService.SaveAsync(category));
        }
        catch (Exception e)
        {
            return StatusCode(500, String.Format(" There was an error saving the category: ${e.Message}"));
        }
    }
    
}