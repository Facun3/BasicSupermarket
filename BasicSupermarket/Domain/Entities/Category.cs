using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Entities;

public class Category: AuditableEntity
{
    [Key]
    public int Id { get; set; }
    [Required, MinLength(3), MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(300)]
    public string Description { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    public virtual Category? Parent { get; set; }
    public virtual ICollection<Category>? Subcategories { get; set; }
    public ICollection<Product>? Products { get; set; }
}