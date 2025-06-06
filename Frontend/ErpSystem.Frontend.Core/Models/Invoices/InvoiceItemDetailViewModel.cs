using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Invoices;

public class InvoiceItemDetailViewModel
{
    public Guid ProductId { get; set; }

    [Display(Name = "Продукт")]
    public string ProductName { get; set; } = string.Empty;

    [Display(Name = "СКУ")]
    public string ProductSku { get; set; } = string.Empty;

    [Display(Name = "Количество")]
    public int Quantity { get; set; }

    [Display(Name = "Единична цена")]
    public decimal UnitPrice { get; set; }

    [Display(Name = "ДДС %")]
    public decimal VatRate { get; set; }

    [Display(Name = "ДДС сума")]
    public decimal VatAmount { get; set; }

    [Display(Name = "Общо")]
    public decimal LineTotal { get; set; }
}
