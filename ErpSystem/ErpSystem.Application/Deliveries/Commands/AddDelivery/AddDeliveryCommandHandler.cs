using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

internal class AddDeliveryCommandHandler : IRequestHandler<AddDeliveryCommand, Guid>
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IUnitOfWork _unitOfWork;

    public AddDeliveryCommandHandler(
        IDeliveryRepository deliveryRepository,
        ISupplierRepository supplierRepository,
        IProductRepository productRepository,
        IInventoryService inventoryService,
        IUnitOfWork unitOfWork
    )
    {
        _deliveryRepository = deliveryRepository;
        _supplierRepository = supplierRepository;
        _productRepository = productRepository;
        _inventoryService = inventoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddDeliveryCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _supplierRepository.GetByIdAsync(
            request.SupplierId,
            cancellationToken
        );

        if (supplier == null)
        {
            throw new NotFoundException(nameof(Supplier), request.SupplierId);
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

        if (products.Count != productIds.Count)
        {
            throw new NotFoundException("One or more products not found.");
        }

        var delivery = new Delivery
        {
            SupplierId = request.SupplierId,
            DeliveryDate = request.DeliveryDate,
            Notes = request.Notes,
            DeliveryItems = request
                .Items.Select(i => new DeliveryItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = products.First(p => p.Id == i.ProductId).Price,
                })
                .ToList(),
            DeliveryStatus = DeliveryStatus.Registered,
        };

        _deliveryRepository.Add(delivery);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return delivery.Id;
    }
}
