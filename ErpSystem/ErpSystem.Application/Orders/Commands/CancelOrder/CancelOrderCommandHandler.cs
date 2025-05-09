using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.CancelOrder;

internal class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelOrderCommandHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork
    )
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id, cancellationToken);

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

        var products = await _productRepository.GetByIdsAsync(
            order.OrderItems.Select(i => i.Id).ToHashSet(),
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

            product.ReservedQuantity -= orderItem.Quantity;

            _productRepository.Update(product);
        }

        order.Status = OrderStatus.Canceled;

        _orderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
