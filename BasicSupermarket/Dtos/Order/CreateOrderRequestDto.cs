namespace BasicSupermarket.Domain.Dto.Order;

public record CreateOrderRequestDto
{
    public string UserId { get; set; }
    public List<OrderItemRequestDto> Items { get; set; }
    public string PaymentMethod { get; set; }
}