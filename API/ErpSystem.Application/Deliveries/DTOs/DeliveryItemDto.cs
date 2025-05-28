namespace ErpSystem.Application.Deliveries.DTOs;

public class DeliveryItemDto : BaseDeliveryItemDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public Guid DeliveryId { get; set; }
}
