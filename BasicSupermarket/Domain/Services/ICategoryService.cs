using BasicSupermarket.Domain.Dto;

namespace BasicSupermarket.Domain.Services;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<Response<CategoryResponseDto>> GetByIdAsync(int id);
    Task<Response<CategoryResponseDto>> CreateAsync(string name);
    Task<Response<CategoryResponseDto>> UpdateAsync(int id, string newName);
    Task<Response<CategoryResponseDto>> DeleteAsync(int id);
}