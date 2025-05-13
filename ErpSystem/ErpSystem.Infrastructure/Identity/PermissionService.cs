using System.Security.Claims;
using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Infrastructure.Identity;

public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRoutePermissionRepository _routePermissionRepository;

    public PermissionService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IRoutePermissionRepository routePermissionRepository
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _routePermissionRepository = routePermissionRepository;
    }

    public async Task<List<Claim>> GetPermissionClaimsAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var roleIds = new List<Guid>();
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                roleIds.Add(role.Id);
            }
        }

        if (!roleIds.Any())
        {
            return new List<Claim>();
        }

        var permissions = new List<RoutePermission>();
        foreach (var roleId in roleIds)
        {
            var rolePermissions = await _routePermissionRepository.GetByRoleIdAsync(
                roleId,
                CancellationToken.None
            );
            permissions.AddRange(rolePermissions);
        }

        return permissions
            .Distinct()
            .Select(p => new Claim("Permission", $"{p.ControllerName}.{p.ActionName}"))
            .ToList();
    }
}
