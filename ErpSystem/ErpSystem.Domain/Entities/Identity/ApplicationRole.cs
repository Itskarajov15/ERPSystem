using Microsoft.AspNetCore.Identity;

namespace ErpSystem.Domain.Entities.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ICollection<RoleRoutePermission> RoleRoutePermissions { get; set; } = new List<RoleRoutePermission>();
}
