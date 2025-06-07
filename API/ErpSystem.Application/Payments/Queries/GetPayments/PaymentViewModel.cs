namespace ErpSystem.Application.Payments.Queries.GetPayments;

public class PaymentViewModel
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string PaymentMethodName { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public string? PaymentReference { get; set; }

    public DateTime CreatedAt { get; set; }
}
