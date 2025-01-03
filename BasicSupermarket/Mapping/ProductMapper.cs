using BasicSupermarket.Domain.Dto;
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
            Price = createProductRequestDto.Price,
            CategoryId = createProductRequestDto.CategoryId,
        };
        return product;
    }

    public static Product FromUpdateProductRequestDtoToProduct(int id, UpdateProductRequestDto updateProductRequestDto)
    {
        Product product = new Product
        {
            Id = id,
            Name = updateProductRequestDto.Name,
            Description = updateProductRequestDto.Description,
            ImageUrl = updateProductRequestDto.ImageUrl,
            Price = updateProductRequestDto.Price,
            CategoryId = updateProductRequestDto.CategoryId
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