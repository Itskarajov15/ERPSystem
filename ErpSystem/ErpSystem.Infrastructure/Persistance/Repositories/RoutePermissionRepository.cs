using ErpSystem.Domain.Entities.Identity;
using ErpSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ErpSystem.Infrastructure.Persistance.Repositories;

public class RoutePermissionRepository : Repository<RoutePermission>, IRoutePermissionRepository
{
    public RoutePermissionRepository(ApplicationDbContext context)
        : base(context) { }

    public async Task<IReadOnlyList<RoutePermission>> GetByRoleIdAsync(
        Guid roleId,
        CancellationToken cancellationToken
    ) =>
        await _context
            .Set<RoutePermission>()
            .Where(rp => rp.RoleRoutePermissions.Any(rrp => rrp.RoleId == roleId))
            .ToListAsync(cancellationToken);
}
