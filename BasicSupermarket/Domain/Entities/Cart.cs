using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Entities;

public class Cart: AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal TotalPrice { get; set; }
}