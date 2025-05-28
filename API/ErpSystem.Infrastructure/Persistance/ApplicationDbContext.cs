using System;
using System.Reflection;
using ErpSystem.Application.Common.Interfaces;
using ErpSystem.Domain.Abstractions;
using ErpSystem.Domain.Entities.Deliveries;
using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Entities.Inventory;
using ErpSystem.Domain.Entities.Sales;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTime dateTime
    )
        : base(options)
    {
        _currentUserService = currentUserService;
        _dateTime = dateTime;
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<UnitOfMeasure> UnitOfMeasures => Set<UnitOfMeasure>();

    public DbSet<Supplier> Suppliers => Set<Supplier>();

    public DbSet<Delivery> Deliveries => Set<Delivery>();

    public DbSet<DeliveryItem> DeliveryItems => Set<DeliveryItem>();

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

    public DbSet<RoutePermission> RoutePermissions => Set<RoutePermission>();

    public DbSet<RoleRoutePermission> RoleRoutePermission => Set<RoleRoutePermission>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId ?? "system";
                    entry.Entity.CreatedAt = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModifiedAt = _dateTime.Now;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
