namespace ErpSystem.Application.Invoices.DTOs;

public class InvoiceItemDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string ProductSku { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal VatRate { get; set; }

    public decimal VatAmount { get; set; }

    public decimal LineTotal { get; set; }
}
