namespace ErpSystem.Frontend.Core.Models.Invoices;

public class CreateInvoiceFromOrderRequest
{
    public Guid OrderId { get; set; }
    public string? Notes { get; set; }
} 