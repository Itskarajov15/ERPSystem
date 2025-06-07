using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Customers;

public class CustomerEditModel
{
    public Guid? Id { get; set; }

    [Required(ErrorMessage = "Името е задължително")]
    [Display(Name = "Име")]
    [StringLength(100, ErrorMessage = "Името не може да бъде по-дълго от 100 символа")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Лицето за контакт е задължително")]
    [Display(Name = "Лице за контакт")]
    [StringLength(
        100,
        ErrorMessage = "Името на лицето за контакт не може да бъде по-дълго от 100 символа"
    )]
    public string ContactName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имейлът е задължителен")]
    [Display(Name = "Имейл")]
    [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефонният номер е задължителен")]
    [Display(Name = "Телефон")]
    [Phone(ErrorMessage = "Невалиден телефонен номер")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Адресът е задължителен")]
    [Display(Name = "Адрес")]
    public string Address { get; set; } = string.Empty;
}
