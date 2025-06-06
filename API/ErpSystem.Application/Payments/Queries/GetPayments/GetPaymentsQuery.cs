using ErpSystem.Domain.Common.Filters;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.Payments.Queries.GetPayments;

public record GetPaymentsQuery(
    PaginationParams PaginationParams,
    PaymentFilters? PaymentFilters = null
) : IRequest<PageResult<PaymentViewModel>>; 