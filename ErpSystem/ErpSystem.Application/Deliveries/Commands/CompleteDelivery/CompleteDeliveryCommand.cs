using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.CompleteDelivery;

public record CompleteDeliveryCommand(Guid Id) : IRequest;
