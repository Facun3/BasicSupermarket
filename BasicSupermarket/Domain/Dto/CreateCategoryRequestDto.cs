using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Dto;

public record CreateCategoryRequestDto
{
    [Required(ErrorMessage = "Name is required"), MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Description is required"), MaxLength(300)]
    public string Description { get; set; } = null!;
    public int? ParentId { get; set; }
}