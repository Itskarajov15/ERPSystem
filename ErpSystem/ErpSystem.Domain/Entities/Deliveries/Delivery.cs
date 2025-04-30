using ErpSystem.Domain.Entities.Common;

namespace ErpSystem.Domain.Entities.Deliveries;

public class Delivery : BaseEntity
{
    public DateTime DeliveryDate { get; set; }

    public Guid SupplierId { get; set; }

    public string? Notes { get; set; }

    public Supplier Supplier { get; set; } = null!;

    public ICollection<DeliveryItem> DeliveryItems { get; set; } = new HashSet<DeliveryItem>();
}
