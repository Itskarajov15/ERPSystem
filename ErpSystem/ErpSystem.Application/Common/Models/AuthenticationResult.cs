namespace ErpSystem.Application.Common.Models;

public class AuthenticationResult
{
    public string UserId { get; set; } = string.Empty;

    public string? UserName { get; set; }

    public string AccessToken { get; set; } = string.Empty;
}
