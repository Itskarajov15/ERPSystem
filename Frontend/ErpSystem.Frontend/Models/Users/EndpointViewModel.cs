namespace ErpSystem.Frontend.Web.Models.Users;

public class EndpointViewModel
{
    public string Id { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public string ControllerName { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public string DisplayName => $"{ControllerName}.{ActionName} ({Endpoint})";
} 