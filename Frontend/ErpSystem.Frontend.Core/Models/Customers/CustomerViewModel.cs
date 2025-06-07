using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Customers;

public class CustomerViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Име")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Лице за контакт")]
    public string ContactName { get; set; } = string.Empty;

    [Display(Name = "Имейл")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Телефон")]
    public string Phone { get; set; } = string.Empty;

    [Display(Name = "Адрес")]
    public string Address { get; set; } = string.Empty;
}
