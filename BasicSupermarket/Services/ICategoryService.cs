using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> ListAsync();
    Task<Response<Category>> SaveAsync(Category category);
    Task<Response<Category>> UpdateAsync(int id, Category category);
    Task<Response<Category>> DeleteAsync(int id);
}