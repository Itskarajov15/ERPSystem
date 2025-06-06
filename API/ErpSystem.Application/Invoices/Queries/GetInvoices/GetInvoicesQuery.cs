using ErpSystem.Application.Invoices.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Invoices.Queries.GetInvoices;

public record GetInvoicesQuery(PaginationParams PaginationParams, InvoiceFilters? Filters)
    : IRequest<PageResult<InvoiceDto>>;
