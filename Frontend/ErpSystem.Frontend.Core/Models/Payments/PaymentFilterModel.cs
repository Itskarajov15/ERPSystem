namespace ErpSystem.Frontend.Core.Models.Payments;

public class PaymentFilterModel
{
    public string? InvoiceNumber { get; set; }

    public Guid? CustomerId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 25;
}
