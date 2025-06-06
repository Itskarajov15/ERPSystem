using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Inventory;

namespace ErpSystem.Domain.Entities.Financial;

public class InvoiceItem : BaseEntity
{
    public Guid InvoiceId { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductSku { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal VatRate { get; set; }

    public decimal VatAmount { get; set; }

    public decimal LineTotal { get; set; }

    public Invoice Invoice { get; set; } = null!;

    public Product Product { get; set; } = null!;
} 