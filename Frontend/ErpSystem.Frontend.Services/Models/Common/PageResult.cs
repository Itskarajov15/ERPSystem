namespace ErpSystem.Frontend.Core.Models.Common;

public class PageResult<T>
{
    public int TotalCount { get; set; }

    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages;

    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
} 