using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Domain.Interfaces;
using ErpSystem.Infrastructure.Identity;
using ErpSystem.Infrastructure.Persistance;
using ErpSystem.Infrastructure.Persistance.Repositories;
using ErpSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ErpSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
            )
        );

        // Register repository
        services.AddScoped<IRepository, Repository>();

        // Register services
        //services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDateTime, DateTimeService>();
        services.AddScoped<IInventoryService, InventoryService>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IPermissionService, PermissionService>();

        // Register current user service
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
