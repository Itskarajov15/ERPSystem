namespace ErpSystem.Application.Deliveries.DTOs;

public class DeliveryItemDto
{
    public Guid Id { get; set; }

    public Guid DeliveryId { get; set; }

    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Sku { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}
