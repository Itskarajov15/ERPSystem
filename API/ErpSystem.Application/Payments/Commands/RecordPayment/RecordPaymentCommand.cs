using MediatR;

namespace ErpSystem.Application.Payments.Commands.RecordPayment;

public record RecordPaymentCommand(
    Guid InvoiceId,
    decimal Amount,
    Guid PaymentMethodId,
    DateTime PaymentDate,
    string? PaymentReference = null
) : IRequest;
