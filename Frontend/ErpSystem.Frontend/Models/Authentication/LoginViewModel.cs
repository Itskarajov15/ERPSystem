using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Web.Models.Authentication;

public class LoginViewModel
{
    [Required(ErrorMessage = "Потребителското име е задължително")]
    [Display(Name = "Потребителско име")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Паролата е задължителна")]
    [Display(Name = "Парола")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Запомни ме")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
} 