using ErpSystem.Application.Suppliers.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Suppliers.Queries.GetSuppliers;

public record GetSuppliersQuery(PaginationParams PaginationParams, SupplierFilters? SupplierFilters)
    : IRequest<PageResult<SupplierDto>>;
