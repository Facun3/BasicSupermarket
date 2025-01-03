using BasicSupermarket.Domain.Dto;
using BasicSupermarket.Domain.Entities;

namespace BasicSupermarket.Mapping;

public static class CategoryMapper
{
    public static CategoryResponseDto FromCategoryToCategoryResponseDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public static IEnumerable<CategoryResponseDto> FromCategoryToCategoryResponseDto(IEnumerable<Category> categories)
    {
        return categories.Select(FromCategoryToCategoryResponseDto).ToList();
    }
}