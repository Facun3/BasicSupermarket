namespace BasicSupermarket.Domain.Dto.Order;

public class OrderResponseDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<OrderItemResponseDto> Items { get; set; }
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}