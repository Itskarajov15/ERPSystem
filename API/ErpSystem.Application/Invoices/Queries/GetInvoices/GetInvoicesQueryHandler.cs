using ErpSystem.Application.Invoices.DTOs;
using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Invoices.Queries.GetInvoices;

internal class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, PageResult<InvoiceDto>>
{
    private readonly IRepository _repository;

    public GetInvoicesQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<InvoiceDto>> Handle(
        GetInvoicesQuery request,
        CancellationToken cancellationToken
    )
    {
        var filterBy = ComposeFilterBy(request.Filters);

        var result = await _repository.GetPaginatedAsync<Invoice, InvoiceDto>(
            request.PaginationParams,
            x =>
                x.Select(i => new InvoiceDto
                {
                    Id = i.Id,
                    InvoiceNumber = i.InvoiceNumber,
                    InvoiceDate = i.InvoiceDate,
                    StatusName = i.Status.ToString(),
                    CustomerName = i.Customer.Name,
                    TotalAmount = i.TotalAmount,
                    Notes = i.Notes,
                    CreatedAt = i.CreatedAt,
                    OrderId = i.OrderId,
                }),
            filterBy
        );

        return result;
    }

    private Func<IQueryable<Invoice>, IQueryable<Invoice>> ComposeFilterBy(
        InvoiceFilters? filters
    ) =>
        query =>
        {
            query = query.Include(i => i.Customer);

            if (filters == null)
            {
                return query;
            }
            if (!string.IsNullOrEmpty(filters.SearchTerm))
            {
                query = query.Where(i =>
                    i.InvoiceNumber.ToLower().Contains(filters.SearchTerm.ToLower())
                );
            }
            if (filters.FromDate.HasValue)
            {
                query = query.Where(i => i.CreatedAt >= filters.FromDate);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(i => i.CreatedAt <= filters.ToDate);
            }
            if (filters.Status.HasValue)
            {
                query = query.Where(i => i.Status == filters.Status);
            }

            return query;
        };
}
