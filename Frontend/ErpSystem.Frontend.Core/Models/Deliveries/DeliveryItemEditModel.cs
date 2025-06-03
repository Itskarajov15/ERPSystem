using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryItemEditModel
{
    [Required(ErrorMessage = "Продуктът е задължителен")]
    [Display(Name = "Продукт")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Количеството е задължително")]
    [Display(Name = "Количество")]
    [Range(1, int.MaxValue, ErrorMessage = "Количеството трябва да бъде по-голямо от 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Единичната цена е задължителна")]
    [Display(Name = "Единична цена")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Единичната цена трябва да бъде по-голяма от 0")]
    public decimal UnitPrice { get; set; }
}
