using ErpSystem.Frontend.Core.Models.Roles;

namespace ErpSystem.Frontend.Core.Models.Users;

public class RoleViewModel
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public List<PermissionViewModel>? Permissions { get; set; }
}
