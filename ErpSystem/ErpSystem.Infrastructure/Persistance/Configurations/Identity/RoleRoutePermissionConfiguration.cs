using ErpSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Identity;

public class RoleRoutePermissionConfiguration : IEntityTypeConfiguration<RoleRoutePermission>
{
    public void Configure(EntityTypeBuilder<RoleRoutePermission> builder)
    {
        builder.HasKey(rrp => new { rrp.RoleId, rrp.RoutePermissionId });

        builder
            .HasOne(rrp => rrp.ApplicationRole)
            .WithMany(r => r.RoleRoutePermissions)
            .HasForeignKey(rrp => rrp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(rrp => rrp.RoutePermission)
            .WithMany(rp => rp.RoleRoutePermissions)
            .HasForeignKey(rrp => rrp.RoutePermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
