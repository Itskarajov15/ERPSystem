using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.CompleteOrder;

internal class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

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

        var products = await _productRepository.GetByIdsAsync(
            order.OrderItems.Select(p => p.Id).ToHashSet(),
            cancellationToken
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

            _productRepository.Update(product);
        }

        order.Status = OrderStatus.Completed;

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
