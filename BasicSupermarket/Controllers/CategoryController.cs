using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class CategoryController(ICategoryService categoryService): BaseApiController
{
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return Ok(await categoryService.ListAsync());
    }
    
}