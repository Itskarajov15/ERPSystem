using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Products;

public class ProductFilterModel
{
    [Display(Name = "Търсене")]
    public string? SearchTerm { get; set; }

    [Display(Name = "Само с ниска наличност")]
    public bool? OnlyLowStock { get; set; }

    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
} 