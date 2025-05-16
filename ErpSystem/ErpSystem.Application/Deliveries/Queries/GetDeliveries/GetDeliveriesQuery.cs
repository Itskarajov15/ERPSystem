using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveries;

public record GetDeliveriesQuery(PaginationParams PaginationParams, DeliveryFilters? Filter)
    : IRequest<PageResult<DeliveryDto>>;
