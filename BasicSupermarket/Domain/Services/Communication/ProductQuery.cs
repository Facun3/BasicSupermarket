namespace BasicSupermarket.Domain.Communication;

public record ProductQuery
{
    public string? SearchFor { get; init; }
    public int? CategoryId { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    // public List<string> Tags { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}