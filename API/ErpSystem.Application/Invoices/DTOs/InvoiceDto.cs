namespace ErpSystem.Application.Invoices.DTOs;

public class InvoiceDto
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
}
