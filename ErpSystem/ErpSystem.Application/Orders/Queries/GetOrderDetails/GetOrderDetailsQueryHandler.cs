using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Orders.Queries.GetOrderDetails;

internal class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailDto>
{
    private readonly IRepository _repository;

    public GetOrderDetailsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<OrderDetailDto> Handle(
        GetOrderDetailsQuery request,
        CancellationToken cancellationToken
    )
    {
        var order = await _repository
            .AllReadOnly<Order>()
            .Include(o => o.Customer)
            .Include(o => o.PaymentMethod)
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.Product)
            .ThenInclude(p => p.UnitOfMeasure)
            .FirstOrDefaultAsync(o => o.Id == request.Id);

        if (order == null)
        {
            throw new NotFoundException(nameof(Order), request.Id);
        }

        return new OrderDetailDto()
        {
            Id = order.Id,
            CustomerAddress = order.Customer.Address,
            CustomerEmail = order.Customer.Email,
            CustomerName = order.Customer.Name,
            CustomerPhone = order.Customer.Phone,
            CustomerId = order.CustomerId,
            Notes = order.Notes,
            OrderDate = order.OrderDate,
            PaymentMethodId = order.PaymentMethodId,
            PaymentMethodName = order.PaymentMethod.Name,
            Status = order.Status.ToString(),
            Items = order
                .OrderItems.Select(i => new OrderItemDetailDto()
                {
                    Id = i.Id,
                    ProductName = i.Product.Name,
                    ProductSku = i.Product.Sku,
                    Quantity = i.Quantity,
                    UnitOfMeasure = i.Product.UnitOfMeasure.Name,
                    UnitPrice = i.UnitPrice,
                })
                .ToList(),
        };
    }
}
