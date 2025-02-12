using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;
using BasicSupermarket.Domain.Services.Communication;

namespace BasicSupermarket.Domain.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> ListAsync();
    Task<Response<CategoryResponseDto>> SaveAsync(CreateCategoryRequestDto category);
    Task<Response<CategoryResponseDto>> UpdateAsync(int id, CreateCategoryRequestDto category);
    Task<Response<CategoryResponseDto>> DeleteAsync(int id);
}