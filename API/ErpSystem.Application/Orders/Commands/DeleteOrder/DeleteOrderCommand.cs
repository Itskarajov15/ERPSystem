using MediatR;

namespace ErpSystem.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid Id) : IRequest;
