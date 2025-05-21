namespace ErpSystem.Domain.Common.Pagination;

public class PageResult<T>
{
    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalCount;
}
