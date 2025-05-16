using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        var delivery = await _repository.GetByIdAsync<Delivery>(request.Id);
        if (delivery == null)
        {
            throw new NotFoundException(nameof(Delivery), request.Id);
        }

        if (delivery.DeliveryStatus != DeliveryStatus.Registered)
        {
            throw new InvalidOperationException("Only registered deliveries can be confirmed.");
        }

        delivery.DeliveryStatus = DeliveryStatus.Completed;

        await _inventoryService.IncreaseStockOfMultipleItemsAsync(
            delivery.DeliveryItems.Select(i => (i.ProductId, i.Quantity)).ToList(),
            cancellationToken
        );

        await _repository.SaveChangesAsync();
    }
}
