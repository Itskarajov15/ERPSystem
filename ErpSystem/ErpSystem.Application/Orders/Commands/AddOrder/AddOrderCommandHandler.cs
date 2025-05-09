using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using ErpSystem.Domain.Interfaces.Repositories;
using MediatR;

namespace ErpSystem.Application.Orders.Commands.AddOrder;

internal class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IProductRepository _productRepository;
    private readonly IInventoryService _inventoryService;
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderCommandHandler(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IProductRepository productRepository,
        IInventoryService inventoryService,
        IUnitOfWork unitOfWork
    )
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _productRepository = productRepository;
        _inventoryService = inventoryService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(AddOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(
            request.CustomerId,
            cancellationToken
        );

        if (customer == null)
        {
            throw new NotFoundException(nameof(Customer), request.CustomerId);
        }

        var paymentMethod = await _paymentMethodRepository.GetByIdAsync(
            request.PaymentMethodId,
            cancellationToken
        );

        if (paymentMethod == null)
        {
            throw new NotFoundException(nameof(PaymentMethod), request.PaymentMethodId);
        }

        var productIds = request.Items.Select(i => i.ProductId).ToList();
        var products = await _productRepository.GetByIdsAsync(productIds, cancellationToken);

        if (products.Count != productIds.Count)
        {
            throw new NotFoundException("One or more products not found.");
        }

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            var availableQuantity = product.Quantity - product.ReservedQuantity;

            if (availableQuantity < item.Quantity)
            {
                throw new InvalidOperationException(
                    $"Insufficient available inventory for product {product.Name}."
                );
            }
        }

        var order = new Order
        {
            CustomerId = request.CustomerId,
            PaymentMethodId = request.PaymentMethodId,
            OrderDate = DateTime.UtcNow,
            Status = OrderStatus.Pending,
            OrderItems = request
                .Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                })
                .ToList(),
        };

        _orderRepository.Add(order);

        foreach (var item in request.Items)
        {
            var product = products.First(p => p.Id == item.ProductId);
            product.ReservedQuantity += item.Quantity;
            _productRepository.Update(product);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
