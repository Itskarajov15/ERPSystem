using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderItemDetailViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Продукт")]
    public string ProductName { get; set; } = string.Empty;

    [Display(Name = "СКУ")]
    public string ProductSku { get; set; } = string.Empty;

    [Display(Name = "Мерна единица")]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [Display(Name = "Количество")]
    public int Quantity { get; set; }

    [Display(Name = "Единична цена")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "Обща цена")]
    public decimal TotalPrice => Quantity * UnitPrice;
}
