namespace BasicSupermarket.Domain.Dto;

public record CategoryResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
}