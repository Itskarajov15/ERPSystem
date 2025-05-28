using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.DeleteDelivery;

public record DeleteDeliveryCommand(Guid Id) : IRequest;
