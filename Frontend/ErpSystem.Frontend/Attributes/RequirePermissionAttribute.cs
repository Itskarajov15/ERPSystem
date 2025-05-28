using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ErpSystem.Frontend.Web.Services;

namespace ErpSystem.Frontend.Web.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RequirePermissionAttribute : TypeFilterAttribute
{
    public RequirePermissionAttribute() : base(typeof(RequirePermissionFilter))
    {
    }

    private class RequirePermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly IPermissionService _permissionService;

        public RequirePermissionFilter(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                context.Result = new ForbidResult();
                return Task.CompletedTask;
            }

            if (!_permissionService.HasPermission(controller, action))
            {
                context.Result = new ForbidResult();
            }

            return Task.CompletedTask;
        }
    }
} 