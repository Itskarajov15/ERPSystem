using System.Security.Claims;
using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Identity;

public class PermissionService : IPermissionService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IRepository _repository;

    public PermissionService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IRepository repository
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _repository = repository;
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
            var rolePermissions = await _repository
                .AllReadOnly<RoutePermission>()
                .Include(rp => rp.RoleRoutePermissions)
                .Where(rp => rp.RoleRoutePermissions.Any(rrp => rrp.RoleId == roleId))
                .ToListAsync();

            permissions.AddRange(rolePermissions);
        }

        return permissions
            .Distinct()
            .Select(p => new Claim("Permission", $"{p.ControllerName}.{p.ActionName}"))
            .ToList();
    }
}
