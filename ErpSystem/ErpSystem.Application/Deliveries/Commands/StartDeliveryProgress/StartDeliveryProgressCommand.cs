using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.StartDeliveryProgress;

public record StartDeliveryProgressCommand(Guid Id) : IRequest;
