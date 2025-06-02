namespace ErpSystem.Frontend.Core.Interfaces;

public interface IPermissionService
{
    bool HasPermission(string controller, string action);
    bool IsInRole(string role);
}
