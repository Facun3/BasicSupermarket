namespace BasicSupermarket.Domain.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTime { get; set; }

    // Foreign Keys
    public int ProductId { get; set; }
    public Product Product { get; set; } 

    public int OrderId { get; set; }
    public Order Order { get; set; }
}