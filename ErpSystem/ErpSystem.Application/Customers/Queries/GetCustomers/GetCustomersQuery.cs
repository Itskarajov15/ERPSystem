using ErpSystem.Application.Customers.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Customers.Queries.GetCustomers;

public record GetCustomersQuery(PaginationParams PaginationParams, CustomerFilters? CustomerFilters)
    : IRequest<PageResult<CustomerDto>>;
