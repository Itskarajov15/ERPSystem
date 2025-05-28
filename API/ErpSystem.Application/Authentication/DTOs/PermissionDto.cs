namespace ErpSystem.Application.Authentication.DTOs;

public class PermissionDto
{
    public string ActionName { get; set; } = null!;

    public string ControllerName { get; set; } = null!;

    public string Endpoint { get; set; } = null!;
}
