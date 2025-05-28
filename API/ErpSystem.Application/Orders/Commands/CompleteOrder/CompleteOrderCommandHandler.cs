using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

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
        var order = await _repository.GetByIdAsync<Order>(request.Id);

        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidOperationException(
                $"Order with ID {request.Id} is not in a state that can be completed."
            );
        }

        var products = await _repository.GetByIdsAsync<Product>(
            order.OrderItems.Select(p => p.Id).ToHashSet()
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

            product.Quantity -= orderItem.Quantity;
            product.ReservedQuantity -= orderItem.Quantity;

            if (product.Quantity < 0 || product.ReservedQuantity < 0)
            {
                throw new InvalidOperationException(
                    $"Product with ID {product.Id} has insufficient stock."
                );
            }
        }

        order.Status = OrderStatus.Completed;

        await _repository.SaveChangesAsync();
    }
}
