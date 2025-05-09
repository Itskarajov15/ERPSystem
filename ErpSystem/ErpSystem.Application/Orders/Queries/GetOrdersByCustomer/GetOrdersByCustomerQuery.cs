using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Orders.DTOs;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId, PaginationRequest PaginationRequest)
    : IRequest<PaginatedList<OrderDto>>;
