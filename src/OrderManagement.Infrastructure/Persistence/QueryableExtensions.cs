namespace OrderManagement.Infrastructure.Persistence;

public static class QueryableExtensions
{
    public static PagedResult<T> ToPagedResult<T>(this IEnumerable<T> source, PageFilter page)
    {
        var list = source as IReadOnlyList<T> ?? [.. source];

        var items = list.Skip(page.Skip).Take(page.PageSize).ToList();
        return new PagedResult<T>(items, page.Page, page.PageSize, list.Count);
    }
}