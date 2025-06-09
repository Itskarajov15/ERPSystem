namespace ErpSystem.Application.Authentication.DTOs;

public class PermissionDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ActionName { get; set; } = null!;

    public string ControllerName { get; set; } = null!;

    public string Endpoint { get; set; } = null!;
}
