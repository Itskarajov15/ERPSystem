namespace ErpSystem.Application.Payments.Queries.GetPaymentById;

public class PaymentDetailViewModel
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    public Guid CustomerId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerPhone { get; set; } = string.Empty;

    public string CustomerEmail { get; set; } = string.Empty;

    public string CustomerAddress { get; set; } = string.Empty;

    public decimal InvoiceTotal { get; set; }

    public decimal Amount { get; set; }

    public Guid PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public string? PaymentReference { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
}
