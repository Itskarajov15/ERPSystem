using ErpSystem.Application.PaymentMethods.DTOs;
using ErpSystem.Domain.Common.Pagination;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethods;

internal class GetPaymentMethodsQueryHandler
    : IRequestHandler<GetPaymentMethodsQuery, PageResult<PaymentMethodDto>>
{
    private readonly IRepository _repository;

    public GetPaymentMethodsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<PaymentMethodDto>> Handle(
        GetPaymentMethodsQuery request,
        CancellationToken cancellationToken
    )
    {
        var paymentMethods = await _repository.GetPaginatedAsync<PaymentMethod, PaymentMethodDto>(
            request.PaginationParams,
            q => q.Select(pm => new PaymentMethodDto() { Id = pm.Id, Name = pm.Name })
        );

        return paymentMethods;
    }
}
