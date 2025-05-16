using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(OrderFilters? Filters, PaginationParams PaginationParams)
    : IRequest<PageResult<OrderDto>>;
