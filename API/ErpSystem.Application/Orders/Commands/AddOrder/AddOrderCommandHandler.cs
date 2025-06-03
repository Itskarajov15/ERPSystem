using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.AddOrder;

internal class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Guid>
{
    private readonly IRepository _repository;
    private readonly IInventoryService _inventoryService;

    public AddOrderCommandHandler(IRepository orderRepository, IInventoryService inventoryService)
    {
        _repository = orderRepository;
        _inventoryService = inventoryService;
    }

    public async Task<Guid> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync<Customer>(request.CustomerId);

        if (customer == null)
        {
            throw new NotFoundException(CustomerErrorKeys.CustomerNotFound);
        }

        var paymentMethod = await _repository.GetByIdAsync<PaymentMethod>(request.PaymentMethodId);

        if (paymentMethod == null)
        {
            throw new NotFoundException(PaymentMethodErrorKeys.PaymentMethodNotFound);
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _repository.GetByIdsAsync<Product>(productIds);

        if (products.Count() != productIds.Count)
        {
            throw new NotFoundException(ProductErrorKeys.ProductNotFound);
        }

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            var availableQuantity = product.Quantity - product.ReservedQuantity;

            if (availableQuantity < item.Quantity)
            {
                throw new InvalidOperationException(ProductErrorKeys.InsufficientStock);
            }
        }

        var order = new Order
        {
            CustomerId = request.CustomerId,
            PaymentMethodId = request.PaymentMethodId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            Notes = request.Notes,
            OrderItems = request
                .Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                })
                .ToList(),
        };

        await _repository.AddAsync<Order>(order);

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            product.ReservedQuantity += item.Quantity;
        }

        await _repository.SaveChangesAsync();

        return order.Id;
    }
}
