using ErpSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Identity;

public class RoutePermissionConfiguration : BaseEntityConfiguration<RoutePermission>
{
    public void Configure(EntityTypeBuilder<RoutePermission> builder)
    {
        builder.HasKey(rp => rp.Id);

        builder.Property(rp => rp.ActionName).IsRequired().HasMaxLength(50);

        builder.Property(rp => rp.ControllerName).IsRequired().HasMaxLength(50);

        builder.Property(rp => rp.Endpoint).IsRequired().HasMaxLength(100);

        builder.HasQueryFilter(rp => !rp.IsDeleted);
    }
}
