using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Invoices.Commands.CreateInvoiceFromOrder;

internal class CreateInvoiceFromOrderCommandHandler
    : IRequestHandler<CreateInvoiceFromOrderCommand, Guid>
{
    private readonly IRepository _repository;

    public CreateInvoiceFromOrderCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateInvoiceFromOrderCommand request,
        CancellationToken cancellationToken
    )
    {
        var order = await _repository
            .All<Order>()
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken);

        if (order == null)
        {
            throw new NotFoundException(OrderErrorKeys.OrderNotFound);
        }

        if (!order.CanGenerateInvoice())
        {
            throw new InvalidOperationException(InvoiceErrorKeys.OrderCannotGenerateInvoice);
        }

        var invoiceNumber = await GenerateInvoiceNumberAsync();

        var invoice = new Invoice
        {
            InvoiceNumber = invoiceNumber,
            InvoiceDate = DateTime.UtcNow,
            OrderId = order.Id,
            CustomerId = order.CustomerId,
            Status = InvoiceStatus.Draft,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        foreach (var orderItem in order.OrderItems)
        {
            var vatRate = 20.0m;
            var lineSubTotal = orderItem.Quantity * orderItem.UnitPrice;
            var vatAmount = lineSubTotal * (vatRate / 100);

            var invoiceItem = new InvoiceItem
            {
                InvoiceId = invoice.Id,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.Product.Name,
                ProductSku = orderItem.Product.Sku,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                VatRate = vatRate,
                VatAmount = vatAmount,
                LineTotal = lineSubTotal + vatAmount,
            };

            invoice.InvoiceItems.Add(invoiceItem);
        }

        invoice.SubTotal = invoice.InvoiceItems.Sum(i => i.Quantity * i.UnitPrice);
        invoice.VatAmount = invoice.InvoiceItems.Sum(i => i.VatAmount);
        invoice.TotalAmount = invoice.SubTotal + invoice.VatAmount;

        await _repository.AddAsync(invoice);

        order.Status = OrderStatus.Archived;
        order.Invoice = invoice;

        await _repository.SaveChangesAsync();

        return invoice.Id;
    }

    private async Task<string> GenerateInvoiceNumberAsync()
    {
        var currentYear = DateTime.UtcNow.Year;
        var yearPrefix = $"INV-{currentYear}-";

        var lastInvoice = await _repository
            .All<Invoice>()
            .Where(i => i.InvoiceNumber.StartsWith(yearPrefix))
            .OrderByDescending(i => i.InvoiceNumber)
            .FirstOrDefaultAsync();

        var nextNumber = 1;
        if (lastInvoice != null)
        {
            var lastNumberPart = lastInvoice.InvoiceNumber.Substring(yearPrefix.Length);
            if (int.TryParse(lastNumberPart, out var lastNumber))
            {
                nextNumber = lastNumber + 1;
            }
        }

        return $"{yearPrefix}{nextNumber:D6}";
    }
}
