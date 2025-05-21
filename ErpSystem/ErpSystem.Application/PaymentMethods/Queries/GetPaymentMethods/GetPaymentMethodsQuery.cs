using ErpSystem.Application.PaymentMethods.DTOs;
using ErpSystem.Domain.Common.Pagination;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethods;

public record GetPaymentMethodsQuery(PaginationParams PaginationParams)
    : IRequest<PageResult<PaymentMethodDto>>;
