using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Orders.Commands.CompleteOrder;

internal class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
{
    private readonly IRepository _repository;

    public CompleteOrderCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository
            .All<Order>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

        if (order == null)
        {
            throw new NotFoundException(OrderErrorKeys.OrderNotFound);
        }

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(OrderErrorKeys.OrderCannotBeCompleted);
        }

        var productIds = order.OrderItems.Select(oi => oi.ProductId).ToHashSet();
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        foreach (var orderItem in order.OrderItems)
        {
            var product = products.FirstOrDefault(p => p.Id == orderItem.ProductId);

            if (product is null)
            {
                throw new InvalidOperationException(ProductErrorKeys.ProductNotFound);
            }

            product.Quantity -= orderItem.Quantity;
            product.ReservedQuantity -= orderItem.Quantity;

            if (product.Quantity < 0 || product.ReservedQuantity < 0)
            {
                throw new InvalidOperationException(ProductErrorKeys.ProductStockInsufficient);
            }
        }

        order.Status = OrderStatus.Completed;

        await _repository.SaveChangesAsync();
    }
}
