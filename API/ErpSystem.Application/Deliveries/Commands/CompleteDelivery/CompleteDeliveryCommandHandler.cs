using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Deliveries.Commands.CompleteDelivery;

internal class CompleteDeliveryCommandHandler : IRequestHandler<CompleteDeliveryCommand>
{
    private readonly IRepository _repository;
    private readonly IInventoryService _inventoryService;

    public CompleteDeliveryCommandHandler(
        IRepository repository,
        IInventoryService inventoryService
    )
    {
        _repository = repository;
        _inventoryService = inventoryService;
    }

    public async Task Handle(CompleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _repository
            .All<Delivery>()
            .Include(d => d.DeliveryItems)
            .FirstOrDefaultAsync(d => d.Id == request.Id, cancellationToken);

        if (delivery == null)
        {
            throw new NotFoundException(DeliveryErrorKeys.DeliveryNotFound, request.Id);
        }

        if (delivery.DeliveryStatus != DeliveryStatus.InProgress)
        {
            throw new InvalidOperationException(DeliveryErrorKeys.DeliveryCannotBeCompleted);
        }

        delivery.DeliveryStatus = DeliveryStatus.Completed;

        await _inventoryService.IncreaseStockOfMultipleItemsAsync(
            delivery.DeliveryItems.Select(i => (i.ProductId, i.Quantity)).ToList()
        );

        await _repository.SaveChangesAsync();
    }
}
