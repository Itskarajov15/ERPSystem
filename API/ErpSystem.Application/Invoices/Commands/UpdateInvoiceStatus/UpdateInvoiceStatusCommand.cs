using ErpSystem.Domain.Entities.Financial;
using MediatR;

namespace ErpSystem.Application.Invoices.Commands.UpdateInvoiceStatus;

public record UpdateInvoiceStatusCommand(
    Guid InvoiceId,
    InvoiceStatus Status
) : IRequest; 