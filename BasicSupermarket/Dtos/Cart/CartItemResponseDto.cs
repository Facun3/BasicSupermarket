namespace BasicSupermarket.Domain.Dto.Cart;

public class CartItemResponseDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
}