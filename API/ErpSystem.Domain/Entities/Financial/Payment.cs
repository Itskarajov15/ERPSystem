using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Entities.Financial;

public class Payment : BaseEntity
{
    public Guid InvoiceId { get; set; }

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }

    public DateTime PaymentDate { get; set; }

    public string? PaymentReference { get; set; }

    public Invoice Invoice { get; set; } = null!;

    public PaymentMethod PaymentMethod { get; set; } = null!;
}
