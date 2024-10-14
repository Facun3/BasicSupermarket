namespace BasicSupermarket.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;

    // Foreign Keys
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int PaymentId { get; set; }
    public Payment Payment { get; set; }

    // Navigation Property
    public ICollection<OrderItem> OrderItems { get; set; } 
}