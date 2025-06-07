using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Payments;

public class PaymentViewModel
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    [Display(Name = "Номер на фактура")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Display(Name = "Клиент")]
    public string CustomerName { get; set; } = string.Empty;

    [Display(Name = "Сума")]
    public decimal Amount { get; set; }

    [Display(Name = "Начин на плащане")]
    public string PaymentMethodName { get; set; } = string.Empty;

    [Display(Name = "Дата на плащане")]
    public DateTime PaymentDate { get; set; }

    [Display(Name = "Референция")]
    public string? PaymentReference { get; set; }

    [Display(Name = "Създадено на")]
    public DateTime CreatedAt { get; set; }
}
