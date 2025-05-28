using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Orders.Queries.GetOrders;

internal class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PageResult<OrderDto>>
{
    private readonly IRepository _repository;

    public GetOrdersQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.Filters);

        var result = await _repository.GetPaginatedAsync(
            request.PaginationParams,
            x =>
                x.Select(o => new OrderDto()
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    StatusName = o.Status.ToString(),
                    CustomerName = o.Customer.Name,
                    PaymentMethodName = o.PaymentMethod.Name,
                    TotalAmount = o.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity),
                }),
            filterBy
        );

        return result;
    }

    private Func<IQueryable<Order>, IQueryable<Order>> ComposeFilterBy(OrderFilters? filters) =>
        query =>
        {
            query = query
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .Include(o => o.PaymentMethod);

            if (filters == null)
            {
                return query;
            }
            if (filters.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == filters.CustomerId);
            }
            if (filters.Status.HasValue)
            {
                query = query.Where(o => o.Status == filters.Status);
            }
            if (filters.FromDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= filters.FromDate);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= filters.ToDate);
            }

            return query;
        };
}
