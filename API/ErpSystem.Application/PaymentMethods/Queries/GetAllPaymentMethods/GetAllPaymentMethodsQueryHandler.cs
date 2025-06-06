using ErpSystem.Application.PaymentMethods.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.PaymentMethods.Queries.GetAllPaymentMethods;

public class GetAllPaymentMethodsQueryHandler
    : IRequestHandler<GetAllPaymentMethodsQuery, List<PaymentMethodDto>>
{
    private readonly IRepository _repository;

    public GetAllPaymentMethodsQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<PaymentMethodDto>> Handle(
        GetAllPaymentMethodsQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _repository
            .AllReadOnly<PaymentMethod>()
            .Select(pm => new PaymentMethodDto { Id = pm.Id, Name = pm.Name })
            .ToListAsync(cancellationToken);
    }
}
