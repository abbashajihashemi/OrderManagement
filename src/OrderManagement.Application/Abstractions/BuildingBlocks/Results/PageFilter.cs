namespace OrderManagement.Application.Abstractions.BuildingBlocks.Results;

public class PageFilter
{
    public const int MaxPageSize = 200;

    private readonly int _page = 1;
    private readonly int _pageSize = 20;

    public int Page
    {
        get => _page;
        init => _page = Math.Max(value, 1);
    }

    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = Math.Clamp(value, 1, MaxPageSize);
    }

    public int Skip => (Page - 1) * PageSize;
}