using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Orders;

public class OrderCreateModel
{
    [Required(ErrorMessage = "Клиентът е задължителен")]
    [Display(Name = "Клиент")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "Начинът на плащане е задължителен")]
    [Display(Name = "Начин на плащане")]
    public Guid PaymentMethodId { get; set; }

    [Display(Name = "Бележки")]
    [StringLength(500, ErrorMessage = "Бележките не могат да бъдат повече от 500 символа")]
    public string? Notes { get; set; }

    [Display(Name = "Артикули")]
    [Required(ErrorMessage = "Поне един артикул е задължителен")]
    [MinLength(1, ErrorMessage = "Поне един артикул е задължителен")]
    public List<OrderItemCreateModel> Items { get; set; } = new();
} 