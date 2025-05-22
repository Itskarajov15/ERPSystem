using System.Security.Claims;
using ErpSystem.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ErpSystem.API.Attributes;

public class SecureRouteAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var allowAnonymous = context
            .ActionDescriptor.EndpointMetadata.OfType<IAllowAnonymous>()
            .Any();

        if (allowAnonymous)
        {
            return;
        }

        var user = context.HttpContext.User;

        if (user == null || (!user.Identity?.IsAuthenticated ?? true))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasAuthorize = context.ActionDescriptor.EndpointMetadata.OfType<IAuthorizeData>().Any();

        if (!hasAuthorize)
        {
            return;
        }

        var serviceProvider = context.HttpContext.RequestServices;
        var identityService = serviceProvider.GetRequiredService<IIdentityService>();

        string action = context.RouteData.Values["action"]!.ToString()!;
        string controller = context.RouteData.Values["controller"]!.ToString()!;

        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        foreach (var role in roles)
        {
            var hasAccess = await identityService.CheckRoleAccessAsync(role, action, controller);

            if (hasAccess)
            {
                return;
            }
        }

        context.Result = new ForbidResult();
    }
}
