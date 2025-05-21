namespace BasicSupermarket.Domain.Dto.Cart;

public record AddCartItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}