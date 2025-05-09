using ErpSystem.Application.Orders.DTOs;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.AddOrder;

public record AddOrderCommand(
    Guid CustomerId,
    Guid PaymentMethodId,
    string? Notes,
    List<OrderItemDto> Items
) : IRequest<Guid>;
