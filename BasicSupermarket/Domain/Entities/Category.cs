using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Entities;

public class Category: AuditableEntity
{
    public int Id { get; set; }
    [Required, MinLength(3), MaxLength(30)]
    public string Name { get; set; } = string.Empty;
    
    public ICollection<Product> Products { get; set; } = new List<Product>();
}