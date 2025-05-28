using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Infrastructure.Persistance;
using ErpSystem.Infrastructure.Persistance.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ErpSystem.API.APIConstants;

namespace ErpSystem.API.Extensions;

public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }

    public static async Task SeedAdminUser(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var adminUser = await userManager.FindByNameAsync(AdminUserName);

        if (adminUser is null)
        {
            adminUser = new ApplicationUser()
            {
                Id = Guid.Parse("9C71EA76-4F07-40A6-A694-3EA4CF07CFAF"),
                UserName = AdminUserName,
                Email = AdminUserEmail,
                NormalizedEmail = AdminUserEmail.ToUpper(),
                NormalizedUserName = AdminUserName.ToUpper(),
                FirstName = AdminUserName,
                LastName = AdminUserName,
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true,
                IsActive = true,
            };

            await userManager.CreateAsync(adminUser, AdminPassword);
        }

        if (!await roleManager.RoleExistsAsync(AdminRoleName))
        {
            await roleManager.CreateAsync(new ApplicationRole() { Name = AdminRoleName });
        }

        if (!await userManager.IsInRoleAsync(adminUser, AdminRoleName))
        {
            await userManager.AddToRoleAsync(adminUser, AdminRoleName);
        }
    }

    public static async Task UpdateRoutePermissions(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await dbContext.UpdateRoutePermissions();
    }

    public static async Task SeedAdminRole(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var routePermissions = await dbContext.RoutePermissions.ToListAsync();

        var adminRole = await roleManager.FindByNameAsync(AdminRoleName);

        if (adminRole is null)
        {
            await roleManager.CreateAsync(
                new ApplicationRole()
                {
                    Id = Guid.Parse("9C71EA76-4F07-40A6-A694-3EA4CF07CFAF"),
                    Name = AdminRoleName,
                    Description = AdminRoleName,
                }
            );

            adminRole = await roleManager.FindByNameAsync(AdminRoleName);

            ICollection<RoleRoutePermission> roleRoutePermissions =
                new HashSet<RoleRoutePermission>();

            foreach (var routePermission in routePermissions)
            {
                roleRoutePermissions.Add(
                    new RoleRoutePermission()
                    {
                        ApplicationRole = adminRole!,
                        RoutePermission = routePermission,
                    }
                );
            }

            dbContext.RoleRoutePermission.AddRange(roleRoutePermissions);

            await dbContext.SaveChangesAsync();
        }
        else if (!HasRoleExactPermissions(adminRole!, routePermissions))
        {
            var oldAdminPermissions = await dbContext
                .RoleRoutePermission.Where(rp => rp.RoleId == adminRole.Id)
                .ToListAsync();

            dbContext.RoleRoutePermission.RemoveRange(oldAdminPermissions);

            await dbContext.RoleRoutePermission.AddRangeAsync(
                routePermissions.Select(rp => new RoleRoutePermission()
                {
                    ApplicationRole = adminRole!,
                    RoutePermission = rp,
                })
            );

            await dbContext.SaveChangesAsync();
        }
    }

    private static bool HasRoleExactPermissions(
        ApplicationRole role,
        ICollection<RoutePermission> permissions
    ) =>
        role.RoleRoutePermissions.Count == permissions.Count
        && role.RoleRoutePermissions.All(rp => permissions.Contains(rp.RoutePermission));
}
