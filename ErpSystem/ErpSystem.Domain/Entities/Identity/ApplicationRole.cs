using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public string Description { get; set; } = null!;

    public ICollection<RoleRoutePermission> RoleRoutePermissions { get; set; } =
        new List<RoleRoutePermission>();
}
