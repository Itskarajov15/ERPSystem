using ErpSystem.Frontend.Core.Models.Users;

namespace ErpSystem.Frontend.Core.Models.Roles;

public class RoleDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<PermissionViewModel> Permissions { get; set; } = new();
}

public class PermissionViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
}
