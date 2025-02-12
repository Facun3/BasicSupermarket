using BasicSupermarket.Domain.Communication;
using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Services.Communication;

namespace BasicSupermarket.Domain.Services;

public interface IProductService
{
    Task<QueryResponseDto<ProductResponseDto>> ListAsync(ProductQuery query);
    Task<Response<ProductResponseDto>> GetByIdAsync(int id);
    Task<Response<ProductResponseDto>> CreateAsync(CreateProductRequestDto productRequestDto);
    Task<Response<ProductResponseDto>> UpdateAsync(int id, UpdateProductRequestDto productUpdateDto);
    Task<Response<ProductResponseDto>> DeleteAsync(int id);
}