using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        var order = await _repository.GetByIdAsync<Order>(request.Id);

        if (order is null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Cannot cancel order with status {order.Status}. Only pending orders can be canceled."
            );
        }

        var products = await _repository.GetByIdsAsync<Product>(
            order.OrderItems.Select(i => i.Id).ToHashSet()
        );

        foreach (var product in products)
        {
            var orderItem = order.OrderItems.FirstOrDefault(i => i.Id == product.Id);

            if (orderItem is null)
            {
                throw new InvalidOperationException(
                    $"Order item with ID {product.Id} not found in order."
                );
            }

            product.ReservedQuantity -= orderItem.Quantity;
        }

        order.Status = OrderStatus.Canceled;

        await _repository.SaveChangesAsync();
    }
}
