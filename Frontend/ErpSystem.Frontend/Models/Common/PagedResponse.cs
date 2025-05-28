using System.Collections.Generic;

namespace ErpSystem.Frontend.Web.Models.Common;

public class PagedResponse<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
} 