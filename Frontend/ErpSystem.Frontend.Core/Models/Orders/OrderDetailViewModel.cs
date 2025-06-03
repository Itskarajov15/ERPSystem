using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderDetailViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Дата на поръчка")]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Статус")]
    public string Status { get; set; } = string.Empty;

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

    public Guid PaymentMethodId { get; set; }

    [Display(Name = "Начин на плащане")]
    public string PaymentMethodName { get; set; } = string.Empty;

    [Display(Name = "Артикули")]
    public List<OrderItemDetailViewModel> Items { get; set; } = new();

    [Display(Name = "Общо артикули")]
    public int TotalItems => Items.Sum(i => i.Quantity);

    [Display(Name = "Междинна сума")]
    public decimal Subtotal => Items.Sum(i => i.TotalPrice);
} 