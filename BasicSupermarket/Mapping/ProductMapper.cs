using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Dto.Product;
using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Mapping;

public static class ProductMapper
{

    public static ProductResponseDto FromProductToProductResponseDto(Product product)
    {
        ProductResponseDto productResponseDto = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Price = product.Price,
            CategoryId = product.CategoryId,
        };
        return productResponseDto;
    }

    public static List<ProductResponseDto> FromProductToProductResponseDto(List<Product> products)
    {
        return products.Select(FromProductToProductResponseDto).ToList();
    }
}