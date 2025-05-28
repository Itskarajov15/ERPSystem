namespace ErpSystem.Frontend.Core.Models.Common;

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new();

    public int TotalCount { get; set; }
}
