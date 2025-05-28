using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Web.Models.Deliveries;

public class DeliveryEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Supplier is required")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "Delivery date is required")]
    public DateTime DeliveryDate { get; set; } = DateTime.Today;

    public string? Comment { get; set; }

    [Required(ErrorMessage = "At least one item is required")]
    public List<DeliveryItemEditModel> Items { get; set; } = new();
}

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