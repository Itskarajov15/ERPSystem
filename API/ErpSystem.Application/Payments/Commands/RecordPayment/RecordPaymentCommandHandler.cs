using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Payments.Commands.RecordPayment;

public class RecordPaymentCommandHandler : IRequestHandler<RecordPaymentCommand>
{
    private readonly IRepository _repository;

    public RecordPaymentCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RecordPaymentCommand request, CancellationToken cancellationToken)
    {
        var invoice = await _repository
            .All<Invoice>()
            .Include(i => i.Payment)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
            throw new NotFoundException(InvoiceErrorKeys.InvoiceNotFound);

        if (!invoice.CanRecordPayment())
            throw new InvalidOperationException(PaymentErrorKeys.InvoiceCannotRecordPayment);

        if (invoice.Payment != null)
            throw new InvalidOperationException(PaymentErrorKeys.InvoiceAlreadyFullyPaid);

        if (request.Amount != invoice.TotalAmount)
            throw new InvalidOperationException(PaymentErrorKeys.InvoicePartialPaymentNotAllowed);

        var paymentMethodExists = await _repository
            .AllReadOnly<PaymentMethod>()
            .AnyAsync(pm => pm.Id == request.PaymentMethodId, cancellationToken);

        if (!paymentMethodExists)
            throw new NotFoundException(PaymentMethodErrorKeys.PaymentMethodNotFound);

        var payment = new Payment
        {
            InvoiceId = request.InvoiceId,
            Amount = request.Amount,
            PaymentMethodId = request.PaymentMethodId,
            PaymentDate = request.PaymentDate,
            PaymentReference = request.PaymentReference
        };

        await _repository.AddAsync(payment);

        invoice.Status = InvoiceStatus.Paid;

        await _repository.SaveChangesAsync();
    }
} 