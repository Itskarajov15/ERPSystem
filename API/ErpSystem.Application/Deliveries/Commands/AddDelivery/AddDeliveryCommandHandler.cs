using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Deliveries.Commands.AddDelivery;

internal class AddDeliveryCommandHandler : IRequestHandler<AddDeliveryCommand, Guid>
{
    private readonly IRepository _repository;

    public AddDeliveryCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(AddDeliveryCommand request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync<Supplier>(request.SupplierId);

        if (supplier == null)
        {
            throw new NotFoundException(SupplierErrorKeys.SupplierNotFound, request.SupplierId);
        }

        if (request.Items == null || !request.Items.Any())
        {
            throw new InvalidOperationException(DeliveryErrorKeys.DeliveryItemsRequired);
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        if (products.Count() != productIds.Count)
        {
            throw new NotFoundException(ProductErrorKeys.ProductRequired);
        }

        var delivery = new Delivery
        {
            SupplierId = request.SupplierId,
            DeliveryDate = request.DeliveryDate,
            DeliveryNumber = request.DeliveryNumber,
            Comment = request.Comment,
            DeliveryItems = request
                .Items.Select(item => new DeliveryItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                })
                .ToHashSet(),
            DeliveryStatus = DeliveryStatus.Registered,
        };

        await _repository.AddAsync(delivery);
        await _repository.SaveChangesAsync();

        return delivery.Id;
    }
}
