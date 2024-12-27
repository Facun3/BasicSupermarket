using BasicSupermarket.Domain.Dtos;
using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Domain.Mapping;

public static class ProductMapper
{
    public static Product FromCreateProductRequestDtoToProduct(CreateProductRequestDto createProductRequestDto)
    {
        Product product = new Product
        {
            Name = createProductRequestDto.Name,
            Description = createProductRequestDto.Description,
            ImageUrl = createProductRequestDto.ImageUrl,
        };
        return product;
    }

    public static Product FromUpdateProductRequestDtoToProduct(UpdateProductRequestDto updateProductRequestDto)
    {
        Product product = new Product
        {
            Name = updateProductRequestDto.Name,
            Description = updateProductRequestDto.Description,
            ImageUrl = updateProductRequestDto.ImageUrl,
        };
        return product;
    }

    public static ProductResponseDto FromProductToProductResponseDto(Product product)
    {
        ProductResponseDto productResponseDto = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
        };
        return productResponseDto;
    }

    public static List<ProductResponseDto> FromProductToProductResponseDto(List<Product> products)
    {
        return products.Select(FromProductToProductResponseDto).ToList();
    }
}