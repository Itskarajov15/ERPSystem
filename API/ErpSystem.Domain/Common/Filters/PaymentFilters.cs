namespace ErpSystem.Domain.Common.Filters;

public class PaymentFilters
{
    public string? InvoiceNumber { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    public DateTime? FromDate { get; set; }
    
    public DateTime? ToDate { get; set; }
} 