namespace ErpSystem.Frontend.Core.Models.DTOs;

public class RoleDto
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<PermissionDto> Permissions { get; set; } = new();
}
