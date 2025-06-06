using MediatR;

namespace ErpSystem.Application.Invoices.Commands.CreateInvoiceFromOrder;

public record CreateInvoiceFromOrderCommand(
    Guid OrderId,
    string? Notes = null
) : IRequest<Guid>; 