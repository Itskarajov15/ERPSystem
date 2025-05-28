namespace ErpSystem.Frontend.Web.Services;

public interface IPermissionService
{
    bool HasPermission(string controller, string action);
    bool IsInRole(string role);
} 