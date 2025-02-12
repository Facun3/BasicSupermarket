using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Dto;

public record CreateProductRequestDto
{
    [Required(ErrorMessage = "Name is required"), MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [MinLength(10), MaxLength(500)]
    public string Description { get; set; } = String.Empty;
    [Required, Url]
    public string ImageUrl { get; set; } = String.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}