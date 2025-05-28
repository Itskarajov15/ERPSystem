using ErpSystem.Domain.Abstractions;

namespace ErpSystem.Domain.Entities.Deliveries;

public class Delivery : BaseEntity
{
    public DateTime DeliveryDate { get; set; }

    public Guid SupplierId { get; set; }

    public string DeliveryNumber { get; set; } = null!;

    public string? Comment { get; set; }

    public DeliveryStatus DeliveryStatus { get; set; }

    public Supplier Supplier { get; set; } = null!;

    public ICollection<DeliveryItem> DeliveryItems { get; set; } = new HashSet<DeliveryItem>();

    public bool CanBeDeleted() => DeliveryStatus == DeliveryStatus.Registered;
}
