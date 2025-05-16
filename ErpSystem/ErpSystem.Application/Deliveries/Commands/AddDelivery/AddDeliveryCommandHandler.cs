using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

internal class AddDeliveryCommandHandler : IRequestHandler<AddDeliveryCommand, Guid>
{
    private readonly IRepository _repository;
    private readonly IInventoryService _inventoryService;

    public AddDeliveryCommandHandler(IRepository repository, IInventoryService inventoryService)
    {
        _repository = repository;
        _inventoryService = inventoryService;
    }

    public async Task<Guid> Handle(AddDeliveryCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync<Supplier>(request.SupplierId);

        if (supplier == null)
        {
            throw new NotFoundException(nameof(Supplier), request.SupplierId);
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        if (products.Count() != productIds.Count)
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

        await _repository.AddAsync<Delivery>(delivery);
        await _repository.SaveChangesAsync();

        return delivery.Id;
    }
}
