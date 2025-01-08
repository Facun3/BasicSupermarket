using BasicSupermarket.Domain;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;
using BasicSupermarket.Repositories;

namespace BasicSupermarket.Services;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger) : ICategoryService
{
    
    public async Task<IEnumerable<Category>> ListAsync()
    {
        return await categoryRepository.GetAllAsync();
    }

    public async Task<Response<Category>> SaveAsync(Category category)
    {
        try
        {
            await categoryRepository.AddAsync(category);
            return new Response<Category>(category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when saving the category");
            return new Response<Category>($"An error occurred when saving the category {ex.Message}");
        }
    }

    public async Task<Response<Category>> UpdateAsync(int id, Category category)
    {
        var existingCategory = await categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return new Response<Category>($"Category with id {id} does not exist");
        }
        existingCategory.Name = category.Name;

        try
        {
            categoryRepository.Update(existingCategory);
            await unitOfWork.CompleteAsync();
            return new Response<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when updating the category");
            return new Response<Category>($"An error occurred when updating the category {ex.Message}");
        }
        
    }

    public async Task<Response<Category>> DeleteAsync(int id)
    {
        var existingCategory = await categoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return new Response<Category>($"Category with id {id} does not exist");
        }

        try
        {
            categoryRepository.Delete(existingCategory);
            await unitOfWork.CompleteAsync();
            return new Response<Category>(existingCategory);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when deleting the category");
            return new Response<Category>($"An error occurred when deleting the category {ex.Message}");
        }
    }
}