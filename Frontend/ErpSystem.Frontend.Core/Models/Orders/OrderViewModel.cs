using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Дата на поръчка")]
    public DateTime OrderDate { get; set; }

    [Display(Name = "Статус")]
    public string StatusName { get; set; } = string.Empty;

    [Display(Name = "Клиент")]
    public string CustomerName { get; set; } = string.Empty;

    [Display(Name = "Начин на плащане")]
    public string PaymentMethodName { get; set; } = string.Empty;

    [Display(Name = "Обща сума")]
    public decimal TotalAmount { get; set; }
}
