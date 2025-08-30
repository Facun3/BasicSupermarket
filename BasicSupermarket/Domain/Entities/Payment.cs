using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicSupermarket.Domain.Entities;

public class Payment: AuditableEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }

    public Order Order { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Method { get; set; }

    [Required]
    public string Status { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
}