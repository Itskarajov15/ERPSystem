namespace ErpSystem.Frontend.Core.Models.Invoices;

public class UpdateInvoiceStatusRequest
{
    public Guid InvoiceId { get; set; }
    public InvoiceStatus Status { get; set; }
}
