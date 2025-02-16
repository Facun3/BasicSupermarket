using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSupermarket.Domain.Entities;

public class CartItem: AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Cart")]
    public int CartId { get; set; }

    public Cart Cart { get; set; }

    [Required]
    public int ProductId { get; set; }
    
    public Product Product { get; set; } 

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal UnitPrice { get; set; }

    [NotMapped]
    public decimal Subtotal => Quantity * UnitPrice;
}