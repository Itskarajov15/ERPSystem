using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderItemCreateModel
{
    [Required(ErrorMessage = "Продуктът е задължителен")]
    [Display(Name = "Продукт")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Количеството е задължително")]
    [Range(1, int.MaxValue, ErrorMessage = "Количеството трябва да бъде по-голямо от 0")]
    [Display(Name = "Количество")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Единичната цена е задължителна")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Единичната цена трябва да бъде по-голяма от 0")]
    [Display(Name = "Единична цена")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "Продукт")]
    public string ProductName { get; set; } = string.Empty;

    [Display(Name = "СКУ")]
    public string ProductSku { get; set; } = string.Empty;

    [Display(Name = "Налично количество")]
    public int AvailableQuantity { get; set; }

    [Display(Name = "Обща цена")]
    public decimal TotalPrice => Quantity * UnitPrice;
}
