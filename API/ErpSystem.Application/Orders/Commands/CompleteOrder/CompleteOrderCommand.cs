using MediatR;

namespace ErpSystem.Application.Orders.Commands.CompleteOrder;

public record CompleteOrderCommand(Guid Id) : IRequest;
