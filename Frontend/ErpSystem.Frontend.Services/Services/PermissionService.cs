using System.Security.Claims;
using ErpSystem.Frontend.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ErpSystem.Frontend.Core.Services;

public class PermissionService : IPermissionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<PermissionService> _logger;

    public PermissionService(
        IHttpContextAccessor httpContextAccessor,
        ILogger<PermissionService> logger
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public bool HasPermission(string controller, string action)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning(
                "User is not authenticated when checking permission for {Controller}.{Action}",
                controller,
                action
            );
            return false;
        }

        var requiredPermission = $"{controller}.{action}";
        var hasPermission = user.Claims.Any(c =>
            c.Type == "Permission"
            && c.Value.Equals(requiredPermission, StringComparison.OrdinalIgnoreCase)
        );

        _logger.LogInformation(
            "Permission check for {Controller}.{Action}: {Result}",
            controller,
            action,
            hasPermission ? "Granted" : "Denied"
        );

        return hasPermission;
    }

    public bool IsInRole(string role)
    {
        var user = _httpContextAccessor.HttpContext?.User;
        if (user == null || !user.Identity?.IsAuthenticated == true)
        {
            _logger.LogWarning("User is not authenticated when checking role {Role}", role);
            return false;
        }

        var isInRole = user.IsInRole(role);
        _logger.LogInformation(
            "Role check for {Role}: {Result}",
            role,
            isInRole ? "Is in role" : "Not in role"
        );

        var userRoles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        _logger.LogInformation(
            "User has the following roles: {Roles}",
            string.Join(", ", userRoles)
        );

        return isInRole;
    }
}
