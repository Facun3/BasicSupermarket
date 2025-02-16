using System.ComponentModel.DataAnnotations;

namespace BasicSupermarket.Domain.Entities;

public class Order: AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();

    [Required]
    public decimal TotalPrice { get; set; }

    [Required]
    public string PaymentMethod { get; set; }

    [Required]
    public string Status { get; set; }

    public Payment Payment { get; set; }
}