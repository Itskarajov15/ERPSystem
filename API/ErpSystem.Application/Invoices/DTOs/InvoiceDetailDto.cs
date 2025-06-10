namespace ErpSystem.Application.Invoices.DTOs;

public class InvoiceDetailDto
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public string StatusName { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerAddress { get; set; } = string.Empty;

    public decimal SubTotal { get; set; }

    public decimal VatAmount { get; set; }

    public decimal TotalAmount { get; set; }

    public List<InvoiceItemDto> Items { get; set; } = new();
}
