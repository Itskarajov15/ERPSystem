using ErpSystem.Domain.Entities.Deliveries;

namespace ErpSystem.Application.Deliveries.DTOs;

public class DeliveryDetailDto
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public string SupplierName { get; set; } = string.Empty;

    public DateTime DeliveryDate { get; set; }

    public string? Notes { get; set; }

    public DeliveryStatus Status { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public bool CanBeDeleted => Status == DeliveryStatus.Registered;

    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

    public List<DeliveryItemDetailDto> Items { get; set; } = new();
}
