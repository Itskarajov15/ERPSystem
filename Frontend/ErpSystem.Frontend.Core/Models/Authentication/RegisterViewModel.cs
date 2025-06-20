using System.ComponentModel.DataAnnotations;

namespace ErpSystem.Frontend.Core.Models.Authentication;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(
        100,
        ErrorMessage = "Password must be at least {2} characters long",
        MinimumLength = 6
    )]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
