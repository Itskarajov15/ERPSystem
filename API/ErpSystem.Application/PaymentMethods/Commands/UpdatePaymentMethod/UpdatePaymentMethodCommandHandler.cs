using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;

namespace ErpSystem.Application.PaymentMethods.Commands.UpdatePaymentMethod;

internal class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand>
{
    private readonly IRepository _repository;

    public UpdatePaymentMethodCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdatePaymentMethodCommand request,
        CancellationToken cancellationToken
    )
    {
        var paymentMethod = await _repository.GetByIdAsync<PaymentMethod>(request.Id);

        if (paymentMethod is null)
        {
            throw new NotFoundException(nameof(PaymentMethod), request.Id);
        }

        paymentMethod.Name = request.Name;

        await _repository.SaveChangesAsync();
    }
}
