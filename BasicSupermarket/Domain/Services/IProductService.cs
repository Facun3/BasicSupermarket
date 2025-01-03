using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;

namespace BasicSupermarket.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> ListAsync(ProductQuery query);
    Task<Response<ProductResponseDto>> GetByIdAsync(int id);
    Task<Response<ProductResponseDto>> CreateAsync(CreateProductRequestDto productRequestDto);
    Task<Response<ProductResponseDto>> UpdateAsync(int id, UpdateProductRequestDto productUpdateDto);
    Task<Response<ProductResponseDto>> DeleteAsync(int id);
}