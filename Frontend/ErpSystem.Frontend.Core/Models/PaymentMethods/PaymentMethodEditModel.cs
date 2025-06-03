using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.PaymentMethods;

public class PaymentMethodEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Името е задължително")]
    [MaxLength(50, ErrorMessage = "Името не може да бъде повече от 50 символа")]
    [Display(Name = "Име")]
    public string Name { get; set; } = string.Empty;
}
