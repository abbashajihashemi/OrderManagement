namespace OrderManagement.Application.Abstractions.BuildingBlocks.Results;

public class PagedResult<T>(
    IEnumerable<T> items,
    int page,
    int pageSize,
    int totalCount)
{
    public IEnumerable<T> Items { get; init; } = items;
    private int TotalCount { get; init; } = totalCount;

    public bool HasPrevious => page > 1;
    public bool HasNext => page < TotalPages;
    private int TotalPages => (int)Math.Ceiling(TotalCount / (double)pageSize);

    public PagedResult<TOut> Map<TOut>(Func<T, TOut> map)
        => new([.. Items.Select(map)], page, pageSize, TotalCount);
}