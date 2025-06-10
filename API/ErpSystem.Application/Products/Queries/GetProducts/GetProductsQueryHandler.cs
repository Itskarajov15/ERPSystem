using ErpSystem.Application.Products.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Products.Queries.GetProducts;

internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, PageResult<ProductDto>>
{
    private readonly IRepository _repository;

    public GetProductsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.ProductFilters);

        var result = await _repository.GetPaginatedAsync(
            request.PaginationParams,
            q =>
                q.Select(p => new ProductDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Sku = p.Sku,
                    Description = p.Description,
                    UnitOfMeasureName = p.UnitOfMeasure.Name,
                    Quantity = p.Quantity - p.ReservedQuantity,
                    ReservedQuantity = p.ReservedQuantity,
                    ReorderLevel = p.ReorderLevel,
                    UnitPrice = p.Price,
                    UnitOfMeasureId = p.UnitOfMeasureId,
                }),
            filterBy
        );

        return result;
    }

    private Func<IQueryable<Product>, IQueryable<Product>> ComposeFilterBy(
        ProductFilters? filters
    ) =>
        query =>
        {
            query = query.Include(p => p.UnitOfMeasure);

            if (filters == null)
            {
                return query;
            }

            if (filters.SearchTerm != null)
            {
                var searchTermLowerCase = filters.SearchTerm.ToLower();

                query = query.Where(p =>
                    p.Name.Contains(searchTermLowerCase)
                    || p.Sku.ToLower().Contains(searchTermLowerCase)
                    || p.Description.ToLower().Contains(searchTermLowerCase)
                );
            }

            if (filters.OnlyLowStock.HasValue)
            {
                if (filters.OnlyLowStock.Value)
                {
                    query = query.Where(p => p.Quantity - p.ReservedQuantity <= p.ReorderLevel);
                }
                else
                {
                    query = query.Where(p => p.Quantity > p.ReorderLevel);
                }
            }

            return query;
        };
}
