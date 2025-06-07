using ErpSystem.Application.Invoices.DTOs;
using MediatR;

namespace ErpSystem.Application.Invoices.Queries.GetInvoiceById;

public record GetInvoiceByIdQuery(Guid Id) : IRequest<InvoiceDetailDto?>;
