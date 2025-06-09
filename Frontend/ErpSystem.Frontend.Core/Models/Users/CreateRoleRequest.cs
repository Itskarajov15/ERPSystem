namespace ErpSystem.Frontend.Core.Models.Users;

public class CreateRoleRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> PermissionIds { get; set; } = new();
}
