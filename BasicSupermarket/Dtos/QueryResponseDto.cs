namespace BasicSupermarket.Domain.Dto;

public record QueryResponseDto<T>
{
    public int Total { get; init; }
    public int Page { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<T> Result { get; init; }
}