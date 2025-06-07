using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Payments;

public class RecordPaymentRequest
{
    public Guid InvoiceId { get; set; }

    [Required(ErrorMessage = "Сумата е задължителна")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Сумата трябва да бъде по-голяма от 0")]
    [Display(Name = "Сума")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Начинът на плащане е задължителен")]
    [Display(Name = "Начин на плащане")]
    public Guid PaymentMethodId { get; set; }

    [Required(ErrorMessage = "Датата на плащане е задължителна")]
    [Display(Name = "Дата на плащане")]
    public DateTime PaymentDate { get; set; } = DateTime.Today;

    [Display(Name = "Референция на плащането")]
    [StringLength(100, ErrorMessage = "Референцията не може да бъде по-дълга от 100 символа")]
    public string? PaymentReference { get; set; }
}
