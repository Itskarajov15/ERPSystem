using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Payments.Queries.GetPayments;

public class GetPaymentsQueryHandler
    : IRequestHandler<GetPaymentsQuery, PageResult<PaymentViewModel>>
{
    private readonly IRepository _repository;

    public GetPaymentsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<PaymentViewModel>> Handle(
        GetPaymentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = _repository
            .AllReadOnly<Payment>()
            .Include(p => p.Invoice)
            .ThenInclude(i => i.Customer)
            .Include(p => p.PaymentMethod)
            .AsQueryable();

        if (request.PaymentFilters != null)
        {
            if (!string.IsNullOrWhiteSpace(request.PaymentFilters.InvoiceNumber))
            {
                query = query.Where(p =>
                    p.Invoice.InvoiceNumber.Contains(request.PaymentFilters.InvoiceNumber)
                );
            }

            if (request.PaymentFilters.CustomerId.HasValue)
            {
                query = query.Where(p =>
                    p.Invoice.CustomerId == request.PaymentFilters.CustomerId.Value
                );
            }

            if (request.PaymentFilters.FromDate.HasValue)
            {
                query = query.Where(p =>
                    p.PaymentDate >= request.PaymentFilters.FromDate.Value.Date
                );
            }

            if (request.PaymentFilters.ToDate.HasValue)
            {
                query = query.Where(p =>
                    p.PaymentDate
                    <= request.PaymentFilters.ToDate.Value.Date.AddDays(1).AddTicks(-1)
                );
            }
        }

        query = query.OrderByDescending(p => p.PaymentDate).ThenByDescending(p => p.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var payments = await query
            .Skip((request.PaginationParams.Page - 1) * request.PaginationParams.PageSize)
            .Take(request.PaginationParams.PageSize)
            .Select(p => new PaymentViewModel
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                InvoiceNumber = p.Invoice.InvoiceNumber,
                CustomerName = p.Invoice.Customer.Name,
                Amount = p.Amount,
                PaymentMethodName = p.PaymentMethod.Name,
                PaymentDate = p.PaymentDate,
                PaymentReference = p.PaymentReference,
                CreatedAt = p.CreatedAt,
            })
            .ToListAsync(cancellationToken);

        return new PageResult<PaymentViewModel>
        {
            Items = payments,
            TotalCount = totalCount,
            CurrentPage = request.PaginationParams.Page,
            PageSize = request.PaginationParams.PageSize,
        };
    }
}
