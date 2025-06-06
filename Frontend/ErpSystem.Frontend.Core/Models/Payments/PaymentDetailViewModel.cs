using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Payments;

public class PaymentDetailViewModel
{
    public Guid Id { get; set; }
    
    public Guid InvoiceId { get; set; }
    
    [Display(Name = "Номер на фактура")]
    public string InvoiceNumber { get; set; } = string.Empty;
    
    [Display(Name = "Дата на фактура")]
    public DateTime InvoiceDate { get; set; }
    
    public Guid CustomerId { get; set; }
    
    [Display(Name = "Клиент")]
    public string CustomerName { get; set; } = string.Empty;
    
    [Display(Name = "Телефон")]
    public string CustomerPhone { get; set; } = string.Empty;
    
    [Display(Name = "Имейл")]
    public string CustomerEmail { get; set; } = string.Empty;
    
    [Display(Name = "Адрес")]
    public string CustomerAddress { get; set; } = string.Empty;
    
    [Display(Name = "Обща сума на фактурата")]
    public decimal InvoiceTotal { get; set; }

    [Display(Name = "Платена сума")]
    public decimal Amount { get; set; }
    
    public Guid PaymentMethodId { get; set; }

    [Display(Name = "Начин на плащане")]
    public string PaymentMethodName { get; set; } = string.Empty;

    [Display(Name = "Дата на плащане")]
    public DateTime PaymentDate { get; set; }

    [Display(Name = "Референция на плащането")]
    public string? PaymentReference { get; set; }

    [Display(Name = "Създадено на")]
    public DateTime CreatedAt { get; set; }
    
    [Display(Name = "Създадено от")]
    public string CreatedBy { get; set; } = string.Empty;
} 