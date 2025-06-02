namespace ErpSystem.Frontend.Core.Models.Customers;

public class CustomerFilterModel
{
    public string? SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
} 