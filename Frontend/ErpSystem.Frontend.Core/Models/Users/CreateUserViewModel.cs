using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Users;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "Имейлът е задължителен")]
    [EmailAddress(ErrorMessage = "Невалиден имейл адрес")]
    [Display(Name = "Имейл адрес")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Потребителското име е задължително")]
    [Display(Name = "Потребителско име")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Името е задължително")]
    [StringLength(50, ErrorMessage = "Името не може да бъде по-дълго от 50 символа")]
    [Display(Name = "Име")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Фамилията е задължителна")]
    [StringLength(50, ErrorMessage = "Фамилията не може да бъде по-дълга от 50 символа")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Паролата е задължителна")]
    [StringLength(
        100,
        ErrorMessage = "Паролата трябва да бъде поне {2} символа",
        MinimumLength = 6
    )]
    [DataType(DataType.Password)]
    [Display(Name = "Парола")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ролята е задължителна")]
    [Display(Name = "Роля")]
    public string SelectedRole { get; set; } = string.Empty;

    public List<RoleViewModel> AvailableRoles { get; set; } = new();
}
