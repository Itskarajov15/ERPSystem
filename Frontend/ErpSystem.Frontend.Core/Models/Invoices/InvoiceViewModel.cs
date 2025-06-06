using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Invoices;

public class InvoiceViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Номер на фактура")]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Display(Name = "Дата на фактура")]
    public DateTime InvoiceDate { get; set; }

    [Display(Name = "Статус")]
    public string StatusName { get; set; } = string.Empty;

    [Display(Name = "Клиент")]
    public string CustomerName { get; set; } = string.Empty;

    [Display(Name = "Обща сума")]
    public decimal TotalAmount { get; set; }

    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
} 