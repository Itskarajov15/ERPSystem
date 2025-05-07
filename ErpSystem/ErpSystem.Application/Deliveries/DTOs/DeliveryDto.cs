using ErpSystem.Domain.Entities.Deliveries;
using static ErpSystem.Application.Deliveries.Commands.AddDelivery.AddDeliveryCommand;

namespace ErpSystem.Application.Deliveries.DTOs;

public class DeliveryDto
{
    public Guid Id { get; set; }

    public Guid SupplierId { get; set; }

    public DateTime DeliveryDate { get; set; }

    public string? Notes { get; set; }

    public DeliveryStatus Status { get; set; }

    public bool CanBeDeleted => Status == DeliveryStatus.Registered;

    public List<DeliveryItemDto> Items { get; set; } = new List<DeliveryItemDto>();
}
