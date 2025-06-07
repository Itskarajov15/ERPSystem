using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.PaymentMethods;

public class PaymentMethodViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Име")]
    public string Name { get; set; } = string.Empty;
}
