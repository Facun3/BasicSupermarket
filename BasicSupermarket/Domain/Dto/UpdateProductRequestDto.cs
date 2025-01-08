using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Dto;

public record UpdateProductRequestDto
{
    [MinLength(3), MaxLength(50)]
    public string Name { get; set; }
    [MinLength(20), MaxLength(200)]
    public string Description { get; set; }
    [Required(ErrorMessage="Please add an image")]
    public string ImageUrl { get; set; }
    public long Price { get; set; }
    public int CategoryId { get; set; }
}