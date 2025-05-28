using ErpSystem.Application.Products.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Products.Queries.GetProducts;

public record GetProductsQuery(PaginationParams PaginationParams, ProductFilters? ProductFilters)
    : IRequest<PageResult<ProductDto>>;
