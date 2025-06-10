using ErpSystem.Application.Common.Constants;
using ErpSystem.Application.Common.Exceptions;
using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Entities.Sales;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Invoices.Commands.UpdateInvoiceStatus;

internal class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand>
{
    private readonly IRepository _repository;

    public UpdateInvoiceStatusCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdateInvoiceStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var invoice = await _repository
            .All<Invoice>()
            .Include(i => i.Order)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, cancellationToken);

        if (invoice == null)
        {
            throw new NotFoundException(InvoiceErrorKeys.InvoiceNotFound);
        }

        switch (request.Status)
        {
            case InvoiceStatus.Issued:
                if (invoice.Status != InvoiceStatus.Draft)
                {
                    throw new InvalidOperationException(InvoiceErrorKeys.InvoiceCannotBeIssued);
                }
                break;

            case InvoiceStatus.Paid:
                if (invoice.Status != InvoiceStatus.Issued)
                {
                    throw new InvalidOperationException(
                        InvoiceErrorKeys.InvoiceCannotBeMarkedAsPaid
                    );
                }
                break;

            case InvoiceStatus.Cancelled:
                if (!invoice.CanBeCancelled())
                {
                    throw new InvalidOperationException(InvoiceErrorKeys.InvoiceCannotBeCancelled);
                }

                if (invoice.Order != null && invoice.Order.Status == OrderStatus.Archived)
                {
                    invoice.Order.Status = OrderStatus.Completed;
                }
                break;
        }

        if (request.Status == InvoiceStatus.Cancelled)
        {
            _repository.Delete(invoice);
        }
        else
        {
            invoice.Status = request.Status;
        }

        await _repository.SaveChangesAsync();
    }
}
