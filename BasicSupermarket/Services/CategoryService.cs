using BasicSupermarket.Domain;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Repositories;
using BasicSupermarket.Domain.Services;
using BasicSupermarket.Domain.Services.Communication;
using BasicSupermarket.Mapping;
using BasicSupermarket.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BasicSupermarket.Services;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger) : ICategoryService
{
    
    public async Task<IEnumerable<CategoryResponseDto>> ListAsync()
    {
        var categories = await categoryRepository.GetQuery().AsNoTracking().ToListAsync();
        return CategoryMapper.FromCategoryToCategoryResponseDto(categories);
    }

    public async Task<Response<CategoryResponseDto>> SaveAsync(CreateCategoryRequestDto createRequest)
    {
        try
        {
            IQueryable<Category> query = categoryRepository.GetQuery();
            var existingCategory = await query.FirstOrDefaultAsync(cat => cat.Name.ToLower() == createRequest.Name.ToLower());
            if (existingCategory != null)
            {
                return new Response<CategoryResponseDto>("Category Name Already Exists");
            }
    
            Category newCategory = new Category{ Name = createRequest.Name, Description = createRequest.Description };
            
            if (createRequest.ParentId != null)
            {
                var existingParent = await query.FirstOrDefaultAsync(cat => cat.Id == createRequest.ParentId);
                if (existingParent == null)
                {
                    return new Response<CategoryResponseDto>("Category Parent Not Found");
                }
                newCategory.ParentId = existingParent.Id;
            }
            
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.CompleteAsync();
            return new Response<CategoryResponseDto>(CategoryMapper.FromCategoryToCategoryResponseDto(newCategory));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when saving the category");
            return new Response<CategoryResponseDto>($"An error occurred when saving the category {ex.Message}");
        }
    }

    public async Task<Response<CategoryResponseDto>> UpdateAsync(int id, CreateCategoryRequestDto updateRequest)
    {
        try
        {
            var existingCategory = await categoryRepository.GetQuery().FirstOrDefaultAsync(cat => cat.Id == id);
            if (existingCategory == null)
            {
                return new Response<CategoryResponseDto>($"Category with id {id} does not exist");
            }
        
            var nameExists = await categoryRepository.GetQuery()
                .AnyAsync(c => c.Name.ToLower() == updateRequest.Name.ToLower() && c.Id != id);
        
            if (nameExists)
            {
                return new Response<CategoryResponseDto>("Category name already exists");
            }
        
            existingCategory.Name = updateRequest.Name.Trim();
            existingCategory.Description = updateRequest.Description.Trim();
            
            categoryRepository.Update(existingCategory);
            await unitOfWork.CompleteAsync();
            return new Response<CategoryResponseDto>(CategoryMapper.FromCategoryToCategoryResponseDto(existingCategory));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when updating the category");
            return new Response<CategoryResponseDto>($"An error occurred when updating the category {ex.Message}");
        }
        
    }

    public async Task<Response<CategoryResponseDto>> DeleteAsync(int id)
    {
        var existingCategory = await categoryRepository.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
        if (existingCategory == null)
        {
            return new Response<CategoryResponseDto>($"Category with id {id} does not exist");
        }

        try
        {
            categoryRepository.Delete(existingCategory);
            await unitOfWork.CompleteAsync();
            return new Response<CategoryResponseDto>(CategoryMapper.FromCategoryToCategoryResponseDto(existingCategory));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when deleting the category");
            return new Response<CategoryResponseDto>($"An error occurred when deleting the category {ex.Message}");
        }
    }
}