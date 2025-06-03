using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.StartDeliveryProgress;

internal class StartDeliveryProgressCommandHandler : IRequestHandler<StartDeliveryProgressCommand>
{
    private readonly IRepository _repository;

    public StartDeliveryProgressCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        StartDeliveryProgressCommand request,
        CancellationToken cancellationToken
    )
    {
        var delivery = await _repository.GetByIdAsync<Delivery>(request.Id);

        if (delivery == null)
        {
            throw new NotFoundException(DeliveryErrorKeys.DeliveryNotFound, request.Id);
        }

        if (delivery.DeliveryStatus != DeliveryStatus.Registered)
        {
            throw new InvalidOperationException(DeliveryErrorKeys.DeliveryCannotBeStarted);
        }

        delivery.DeliveryStatus = DeliveryStatus.InProgress;
        await _repository.SaveChangesAsync();
    }
}
