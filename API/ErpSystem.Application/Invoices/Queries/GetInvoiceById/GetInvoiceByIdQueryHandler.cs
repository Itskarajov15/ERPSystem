using ErpSystem.Application.Invoices.DTOs;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Invoices.Queries.GetInvoiceById;

internal class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, InvoiceDetailDto?>
{
    private readonly IRepository _repository;

    public GetInvoiceByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<InvoiceDetailDto?> Handle(
        GetInvoiceByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var invoice = await _repository
            .All<Invoice>()
            .Include(i => i.Customer)
            .Include(i => i.InvoiceItems)
            .ThenInclude(ii => ii.Product)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice == null)
            return null;

        return new InvoiceDetailDto
        {
            Id = invoice.Id,
            InvoiceNumber = invoice.InvoiceNumber,
            InvoiceDate = invoice.InvoiceDate,
            StatusName = invoice.Status.ToString(),
            Notes = invoice.Notes,
            CreatedAt = invoice.CreatedAt,
            OrderId = invoice.OrderId,
            CustomerId = invoice.CustomerId,
            CustomerName = invoice.Customer.Name,
            CustomerPhone = invoice.Customer.Phone,
            CustomerEmail = invoice.Customer.Email,
            CustomerAddress = invoice.Customer.Address,
            SubTotal = invoice.SubTotal,
            VatAmount = invoice.VatAmount,
            TotalAmount = invoice.TotalAmount,
            Items = invoice
                .InvoiceItems.Select(ii => new InvoiceItemDto
                {
                    ProductId = ii.ProductId,
                    ProductName = ii.ProductName,
                    ProductSku = ii.ProductSku,
                    Quantity = ii.Quantity,
                    UnitPrice = ii.UnitPrice,
                    VatRate = ii.VatRate,
                    VatAmount = ii.VatAmount,
                    LineTotal = ii.LineTotal,
                })
                .ToList(),
        };
    }
}
