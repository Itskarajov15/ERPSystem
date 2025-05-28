using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.DeleteOrder;

internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
    private readonly IRepository _repository;

    public DeleteOrderCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync<Order>(request.Id);

        if (order is null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Cannot delete order with status {order.Status}. Only pending orders can be deleted."
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

        await _repository.SaveChangesAsync();
    }
}
