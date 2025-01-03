namespace BasicSupermarket.Domain.Communication;

public record ProductQuery
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public int? CategoryId { get; init; }
    public string? SearchFor { get; init; }
    
}