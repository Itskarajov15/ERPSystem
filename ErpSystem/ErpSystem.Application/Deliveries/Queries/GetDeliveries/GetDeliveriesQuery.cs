using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveries;

public record GetDeliveriesQuery(PaginationRequest PaginationRequest, DeliveryFilters? Filter)
    : IRequest<PaginatedList<DeliveryDto>>;
