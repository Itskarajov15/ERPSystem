using System.Reflection;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Infrastructure.Comparers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Extensions;

public static class SeedExtensions
{
    public static async Task UpdateRoutePermissions(this ApplicationDbContext context)
    {
        bool flag = false;

        var currentPermissions = await context.RoutePermissions.ToListAsync();
        var permissions = context.GetAllRoutePermissions();
        var permissionComparer = new RoutePermissionComparer();

        var permissionsToRemove = currentPermissions.Except(permissions, permissionComparer);

        if (permissionsToRemove.Any())
        {
            var rolesPermissions = await context.RoleRoutePermission.ToListAsync();

            var linkedPermissions = rolesPermissions.Where(rrp =>
                permissionsToRemove.Any(p => p.Id == rrp.RoutePermission.Id)
            );

            if (linkedPermissions.Any())
            {
                context.RoleRoutePermission.RemoveRange(linkedPermissions);
            }

            context.RoutePermissions.RemoveRange(permissionsToRemove);

            flag = true;
        }

        var permissionsToAdd = permissions.Except(currentPermissions, permissionComparer);

        if (permissionsToAdd.Any())
        {
            await context.RoutePermissions.AddRangeAsync(permissionsToAdd);

            flag = true;
        }

        if (flag)
        {
            await context.SaveChangesAsync();
        }
    }

    private static IEnumerable<RoutePermission> GetAllRoutePermissions(
        this ApplicationDbContext dbContext
    )
    {
        var routePermissions = new List<RoutePermission>();

        var apiAssembly = AppDomain
            .CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == InfrastructureConstants.ApiAssemblyName);

        if (apiAssembly == null)
        {
            throw new Exception(
                $"Assembly with name {InfrastructureConstants.ApiAssemblyName} was not found."
            );
        }

        var controllers = apiAssembly
            .GetExportedTypes()
            .Where(t => !t.IsAbstract && typeof(ControllerBase).IsAssignableFrom(t));

        foreach (var controller in controllers)
        {
            var controllerName = controller.Name.Replace(
                InfrastructureConstants.Controller,
                string.Empty
            );

            var methods = controller
                .GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                .Where(m => m.IsPublic && !m.IsSpecialName);

            foreach (var method in methods)
            {
                routePermissions.Add(
                    new RoutePermission
                    {
                        ControllerName = controllerName,
                        ActionName = method.Name,
                    }
                );
            }
        }

        return routePermissions;
    }
}
