using ErpSystem.Domain.Entities.Identity;

namespace ErpSystem.Domain.Interfaces.Repositories;

public interface IRoutePermissionRepository : IRepository<RoutePermission>
{
    Task<IReadOnlyList<RoutePermission>> GetByRoleIdAsync(
        Guid roleId,
        CancellationToken cancellationToken
    );
}
