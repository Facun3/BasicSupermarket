using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Controllers;

public class CategoryController: BaseApiController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponseDto>), 200)]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return Ok(await _categoryService.GetAllAsync());
    }
    
    
}