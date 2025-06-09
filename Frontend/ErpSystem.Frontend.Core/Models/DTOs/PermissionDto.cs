namespace ErpSystem.Frontend.Core.Models.DTOs;

public class PermissionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ActionName { get; set; } = string.Empty;

    public string ControllerName { get; set; } = string.Empty;

    public string Endpoint { get; set; } = string.Empty;
}
