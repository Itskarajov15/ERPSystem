using ErpSystem.Application.Common.Constants;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.PaymentMethods.Commands.DeletePaymentMethod;

internal class DeletePaymentMethodCommandHandler : IRequestHandler<DeletePaymentMethodCommand>
{
    private readonly IRepository _repository;

    public DeletePaymentMethodCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        DeletePaymentMethodCommand request,
        CancellationToken cancellationToken
    )
    {
        var orders = await _repository
            .AllReadOnly<Order>()
            .Where(o => o.PaymentMethodId == request.Id)
            .ToListAsync();

        if (orders.Any())
        {
            throw new InvalidOperationException(PaymentMethodErrorKeys.PaymentMethodIsUsedByOrders);
        }

        var payments = await _repository
            .AllReadOnly<Payment>()
            .Where(p => p.PaymentMethodId == request.Id)
            .ToListAsync();

        if (payments.Any())
        {
            throw new InvalidOperationException(
                PaymentMethodErrorKeys.PaymentMethodIsUsedByPayments
            );
        }

        await _repository.SoftDeleteById<PaymentMethod>(request.Id);
        await _repository.SaveChangesAsync();
    }
}
