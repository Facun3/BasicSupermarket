using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Dto.Product;

public record CreateProductRequestDto
{
    [Required(ErrorMessage = "Name is required"), MinLength(3), MaxLength(100)]
    public string Name { get; set; } = null!;
    [MinLength(10), MaxLength(500)]
    public string Description { get; set; } = String.Empty;
    [Url]
    public string ImageUrl { get; set; } = String.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Quantity is required"), Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}