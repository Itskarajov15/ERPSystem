namespace ErpSystem.Application.Authentication.DTOs;

public class EditRoleDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<Guid> PermissionIds { get; set; } = new();
}
