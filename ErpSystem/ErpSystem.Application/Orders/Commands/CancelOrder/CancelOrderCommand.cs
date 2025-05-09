using MediatR;

namespace ErpSystem.Application.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid Id) : IRequest;
