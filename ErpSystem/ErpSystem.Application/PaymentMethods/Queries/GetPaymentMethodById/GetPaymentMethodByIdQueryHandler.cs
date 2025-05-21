using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Application.PaymentMethods.DTOs;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Queries.GetPaymentMethodById;

internal class GetPaymentMethodByIdQueryHandler
    : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodDto>
{
    private readonly IRepository _repository;

    public GetPaymentMethodByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentMethodDto> Handle(
        GetPaymentMethodByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var paymentMethod = await _repository.GetByIdAsync<PaymentMethod>(request.Id);

        if (paymentMethod is null)
        {
            throw new NotFoundException(nameof(PaymentMethod), request.Id);
        }

        return new PaymentMethodDto() { Id = paymentMethod.Id, Name = paymentMethod.Name };
    }
}
