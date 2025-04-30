namespace ErpSystem.Domain.Entities.Identity;

public class RoleRoutePermission
{
    public Guid RoleId { get; set; }

    public Guid RoutePermissionId { get; set; }

    public ApplicationRole ApplicationRole { get; set; } = null!;

    public RoutePermission RoutePermission { get; set; } = null!;
}
