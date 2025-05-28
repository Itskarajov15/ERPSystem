using ErpSystem.Application.Common.Models;
using ErpSystem.Application.Deliveries.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Deliveries.Queries.GetDeliveries;

internal class GetDeliveriesQueryHandler
    : IRequestHandler<GetDeliveriesQuery, PageResult<DeliveryDto>>
{
    private readonly IRepository _repository;

    public GetDeliveriesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<DeliveryDto>> Handle(
        GetDeliveriesQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.Filter);

        var result = await _repository.GetPaginatedAsync(
            request.PaginationParams,
            x =>
                x.Select(d => new DeliveryDto()
                {
                    Id = d.Id,
                    SupplierId = d.SupplierId,
                    DeliveryDate = d.DeliveryDate.ToString("dd/MM/yyyy"),
                    DeliveryNumber = d.DeliveryNumber,
                    Comment = d.Comment,
                    Status = d.DeliveryStatus,
                    StatusName = d.DeliveryStatus.ToDisplayName(),
                    SupplierName = d.Supplier.Name,
                }),
            filterBy
        );

        return result;
    }

    private Func<IQueryable<Delivery>, IQueryable<Delivery>> ComposeFilterBy(
        DeliveryFilters? filters
    ) =>
        query =>
        {
            query = query
                .Include(d => d.Supplier)
                .Include(d => d.DeliveryItems)
                .ThenInclude(i => i.Product);

            if (filters == null)
            {
                return query;
            }
            if (filters.SupplierId.HasValue)
            {
                query = query.Where(d => d.SupplierId == filters.SupplierId);
            }
            if (filters.FromDate.HasValue)
            {
                query = query.Where(d => d.CreatedAt >= filters.FromDate);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(d => d.CreatedAt <= filters.ToDate);
            }
            if (filters.Status.HasValue)
            {
                query = query.Where(d => d.DeliveryStatus == filters.Status);
            }
            return query;
        };
}
