using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Products.Commands.DeleteProduct;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IRepository _repository;

    public DeleteProductCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var orderItems = await _repository
            .AllReadOnly<OrderItem>()
            .Where(o => o.ProductId == request.ProductId)
            .ToListAsync(cancellationToken);

        if (orderItems.Any())
        {
            throw new InvalidOperationException(ProductErrorKeys.ProductExistsInOrders);
        }

        var deliveryItems = await _repository
            .AllReadOnly<DeliveryItem>()
            .Where(d => d.ProductId == request.ProductId)
            .ToListAsync(cancellationToken);

        if (deliveryItems.Any())
        {
            throw new InvalidOperationException(ProductErrorKeys.ProductExistsInDeliveries);
        }

        await _repository.SoftDeleteById<Product>(request.ProductId);
        await _repository.SaveChangesAsync();
    }
}
