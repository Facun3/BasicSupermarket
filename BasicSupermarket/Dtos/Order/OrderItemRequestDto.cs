namespace BasicSupermarket.Domain.Dto.Order;

public record OrderItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}