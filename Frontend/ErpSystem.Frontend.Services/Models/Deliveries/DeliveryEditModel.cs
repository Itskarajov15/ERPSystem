using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Deliveries;

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
