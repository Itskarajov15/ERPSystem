using ErpSystem.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSystem.Infrastructure.Persistance.Configurations.Identity;

public class RoutePermissionConfiguration : BaseEntityConfiguration<RoutePermission>
{
    public override void Configure(EntityTypeBuilder<RoutePermission> builder)
    {
        base.Configure(builder);

        builder.Property(rp => rp.ActionName).IsRequired().HasMaxLength(100);

        builder.Property(rp => rp.ControllerName).IsRequired().HasMaxLength(100);

        builder.HasQueryFilter(rp => !rp.IsDeleted);
    }
}
