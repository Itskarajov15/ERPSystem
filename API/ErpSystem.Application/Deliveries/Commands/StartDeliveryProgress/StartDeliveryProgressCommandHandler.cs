using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var delivery = await _repository
            .AllReadOnly<Delivery>()
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (delivery == null)
        {
            throw new NotFoundException(nameof(Delivery), request.Id);
        }

        if (delivery.DeliveryStatus != DeliveryStatus.Registered)
        {
            throw new InvalidOperationException("Only registered deliveries can be started.");
        }

        delivery.DeliveryStatus = DeliveryStatus.InProgress;
        await _repository.SaveChangesAsync();
    }
}
