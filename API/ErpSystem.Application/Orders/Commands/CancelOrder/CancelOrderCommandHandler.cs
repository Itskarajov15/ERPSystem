using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Orders.Commands.CancelOrder;

internal class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IRepository _repository;

    public CancelOrderCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository
            .All<Order>()
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == request.Id);

        if (order is null)
        {
            throw new NotFoundException(OrderErrorKeys.OrderNotFound);
        }

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(OrderErrorKeys.OrderCannotBeCanceled);
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

            product.ReservedQuantity -= orderItem.Quantity;
        }

        order.Status = OrderStatus.Canceled;

        await _repository.SaveChangesAsync();
    }
}
