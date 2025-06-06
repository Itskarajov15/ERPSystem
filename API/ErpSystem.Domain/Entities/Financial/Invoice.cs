using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Sales;

namespace ErpSystem.Domain.Entities.Financial;

public class Invoice : BaseEntity
{
    public string InvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public InvoiceStatus Status { get; set; }

    public decimal SubTotal { get; set; }

    public decimal VatAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public Order Order { get; set; } = null!;

    public Customer Customer { get; set; } = null!;

    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new HashSet<InvoiceItem>();

    public bool CanBeCancelled() => Status == InvoiceStatus.Draft || Status == InvoiceStatus.Issued;
}
