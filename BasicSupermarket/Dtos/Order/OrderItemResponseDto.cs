namespace BasicSupermarket.Domain.Dto.Order;

public class OrderItemResponseDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}