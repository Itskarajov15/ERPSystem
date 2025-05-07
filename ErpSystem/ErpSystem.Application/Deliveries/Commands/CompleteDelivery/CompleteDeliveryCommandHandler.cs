using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.CompleteDelivery;

internal class CompleteDeliveryCommandHandler : IRequestHandler<CompleteDeliveryCommand>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteDeliveryCommandHandler(
        IDeliveryRepository deliveryRepository,
        IInventoryService inventoryService,
        IUnitOfWork unitOfWork
    )
    {
        _deliveryRepository = deliveryRepository;
        _inventoryService = inventoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CompleteDeliveryCommand request, CancellationToken cancellationToken)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(request.Id, cancellationToken);
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

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
