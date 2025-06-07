using ErpSystem.Domain.Entities.Financial;
using ErpSystem.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Application.Payments.Queries.GetPaymentById;

public class GetPaymentByIdQueryHandler
    : IRequestHandler<GetPaymentByIdQuery, PaymentDetailViewModel?>
{
    private readonly IRepository _repository;

    public GetPaymentByIdQueryHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaymentDetailViewModel?> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var payment = await _repository
            .AllReadOnly<Payment>()
            .Include(p => p.Invoice)
            .ThenInclude(i => i.Customer)
            .Include(p => p.PaymentMethod)
            .Where(p => p.Id == request.PaymentId)
            .Select(p => new PaymentDetailViewModel
            {
                Id = p.Id,
                InvoiceId = p.InvoiceId,
                InvoiceNumber = p.Invoice.InvoiceNumber,
                InvoiceDate = p.Invoice.InvoiceDate,
                CustomerId = p.Invoice.CustomerId,
                CustomerName = p.Invoice.Customer.Name,
                CustomerPhone = p.Invoice.Customer.Phone ?? string.Empty,
                CustomerEmail = p.Invoice.Customer.Email ?? string.Empty,
                CustomerAddress = p.Invoice.Customer.Address ?? string.Empty,
                InvoiceTotal = p.Invoice.TotalAmount,
                Amount = p.Amount,
                PaymentMethodId = p.PaymentMethodId,
                PaymentMethodName = p.PaymentMethod.Name,
                PaymentDate = p.PaymentDate,
                PaymentReference = p.PaymentReference,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
            })
            .FirstOrDefaultAsync(cancellationToken);

        return payment;
    }
}
