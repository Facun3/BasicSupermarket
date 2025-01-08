namespace BasicSupermarket.Domain.Dto;
public record ProductResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
}