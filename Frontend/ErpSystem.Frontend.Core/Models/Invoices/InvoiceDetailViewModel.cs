using System.ComponentModel.DataAnnotations;
using ErpSystem.Frontend.Core.Models.Payments;

namespace ErpSystem.Frontend.Core.Models.Invoices;

public class InvoiceDetailViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Номер на фактура")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Display(Name = "Дата на фактура")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Статус")]
    public string StatusName { get; set; } = string.Empty;

    [Display(Name = "Бележки")]
    public string? Notes { get; set; }

    public Guid CustomerId { get; set; }

    [Display(Name = "Клиент")]
    public string CustomerName { get; set; } = string.Empty;

    [Display(Name = "Телефон")]
    public string CustomerPhone { get; set; } = string.Empty;

    [Display(Name = "Имейл")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Display(Name = "Адрес")]
    public string CustomerAddress { get; set; } = string.Empty;

    [Display(Name = "Междинна сума")]
    public decimal SubTotal { get; set; }

    [Display(Name = "ДДС")]
    public decimal VatAmount { get; set; }

    [Display(Name = "Обща сума")]
    public decimal TotalAmount { get; set; }

    [Display(Name = "Артикули")]
    public List<InvoiceItemDetailViewModel> Items { get; set; } = new();

    [Display(Name = "Плащане")]
    public PaymentDetailViewModel? Payment { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OrderId { get; set; }

    public bool CanRecordPayment => StatusName == "Issued" && Payment == null;
    public bool IsFullyPaid => Payment != null && Payment.Amount >= TotalAmount;
}
