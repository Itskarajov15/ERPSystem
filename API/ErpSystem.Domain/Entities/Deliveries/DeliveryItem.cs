using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Domain.Entities.Deliveries;

public class DeliveryItem : BaseEntity
{
    public Guid ProductId { get; set; }

    public Guid DeliveryId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public Product Product { get; set; } = null!;

    public Delivery Delivery { get; set; } = null!;
}
