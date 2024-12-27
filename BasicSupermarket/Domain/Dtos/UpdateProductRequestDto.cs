using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Dtos;

public class UpdateProductRequestDto
{
    [Required(ErrorMessage="Please add an id")]
    public int Id { get; }
    [MinLength(3), MaxLength(50)]
    public String Name { get; }
    [MinLength(20), MaxLength(200)]
    public String Description { get; }
    [Required(ErrorMessage="Please add an image")]
    public String ImageUrl { get; }
}