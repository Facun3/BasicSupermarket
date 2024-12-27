using BasicSupermarket.Domain.Dtos;
using BasicSupermarket.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BasicSupermarket.Services;

public interface IProductService
{
    public Task<IEnumerable<ProductResponseDto>> GetProducts(string? name, int? page = null, int? pageSize = null);
    public Task<ProductResponseDto?> GetProduct(int id);
    public Task<ProductResponseDto> PostProduct(CreateProductRequestDto productRequestDto);
    public Task<ProductResponseDto> PutProduct(UpdateProductRequestDto productUpdateDto);
    public Task<int> DeleteProduct(int id);
}