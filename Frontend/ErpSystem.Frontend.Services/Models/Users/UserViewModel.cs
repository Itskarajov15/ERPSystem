namespace ErpSystem.Frontend.Core.Models.Users;

public class UserViewModel
{
    public string Id { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public List<RoleViewModel> Roles { get; set; } = new();

    public string FullName => $"{FirstName} {LastName}";

    public string Status => IsActive ? "Active" : "Inactive";

    public string LastLoginDisplay => LastLogin?.ToString("g") ?? "Never";
}
