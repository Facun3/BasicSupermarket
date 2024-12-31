using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Repositories;

namespace BasicSupermarket.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService( ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _logger = logger;
    }
    
    public async Task<IEnumerable<Category>> ListAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<Response<Category>> SaveAsync(Category category)
    {
        try
        {
            await _categoryRepository.AddAsync(category);
            return new Response<Category>(category);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred when saving the category");
            return new Response<Category>($"An error occurred when saving the category {ex.Message}");
        }
    }

    public async Task<Response<Category>> UpdateAsync(int id, Category category)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return new Response<Category>($"Category with id {id} does not exist");
        }
        existingCategory.Name = category.Name;

        try
        {
            await _categoryRepository.UpdateAsync(existingCategory);
            return new Response<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred when updating the category");
            return new Response<Category>($"An error occurred when updating the category {ex.Message}");
        }
        
    }

    public async Task<Response<Category>> DeleteAsync(int id)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return new Response<Category>($"Category with id {id} does not exist");
        }

        try
        {
            await _categoryRepository.DeleteAsync(id);
            return new Response<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred when deleting the category");
            return new Response<Category>($"An error occurred when deleting the category {ex.Message}");
        }
    }
}