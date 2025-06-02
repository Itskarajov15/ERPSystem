using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Deliveries;

public class DeliveryItemEditModel
{
    [Required(ErrorMessage = "Product is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Unit price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0")]
    public decimal UnitPrice { get; set; }
}
