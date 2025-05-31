using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Products;

public class ProductViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Име")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "СКУ")]
    public string Sku { get; set; } = string.Empty;

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Display(Name = "Цена")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "Количество")]
    public decimal Quantity { get; set; }

    [Display(Name = "Минимално количество")]
    public decimal ReorderLevel { get; set; }

    public Guid UnitOfMeasureId { get; set; }

    [Display(Name = "Мерна единица")]
    public string UnitOfMeasureName { get; set; } = string.Empty;

    public bool IsLowStock => Quantity < ReorderLevel;
}
