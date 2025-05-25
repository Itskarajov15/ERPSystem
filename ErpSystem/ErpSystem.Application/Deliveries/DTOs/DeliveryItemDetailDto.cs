namespace ErpSystem.Application.Deliveries.DTOs;

public class DeliveryItemDetailDto : BaseDeliveryItemDto
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public string ProductName { get; set; } = string.Empty;

    public decimal TotalPrice => UnitPrice * Quantity;
}
