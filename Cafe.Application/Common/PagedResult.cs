namespace Cafe.Application.Common;

public class PagedResult<T> where T : class
{
    public List<T> Items { get; init; }

    public int TotalPages { get; init; }

    public int ItemsFrom { get; init; }

    public int ItemsTo { get; init; }

    public int TotalItemsCount { get; init; }

    public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }
}
