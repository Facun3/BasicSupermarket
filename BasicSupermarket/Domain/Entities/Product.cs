using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSupermarket.Domain.Entities;

public class Product: AuditableEntity
{
    public int Id { get; set; }

    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [MinLength(10), MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required] [Range(0, int.MaxValue)] public int Quantity { get; set; } = 0;
    
    [Url]
    public string ImageUrl { get; set; } = string.Empty;
    
    public int CategoryId { get; set; }
    
    public Category Category { get; set; } = default!;
    
    public override string ToString()
    {
        return $"Product: {Name}, Description: {Description}";
    }
}
