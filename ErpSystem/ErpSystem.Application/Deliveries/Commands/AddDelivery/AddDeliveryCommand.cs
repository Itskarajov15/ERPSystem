using ErpSystem.Application.Deliveries.DTOs;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

public record AddDeliveryCommand(
    Guid SupplierId,
    DateTime DeliveryDate,
    string? Notes,
    List<AddDeliveryItemDto> Items
) : IRequest<Guid>;
