using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Roles;
using ErpSystem.Frontend.Core.Models.Users;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IRoleService
{
    Task<PageResult<RoleViewModel>> GetRolesAsync(RoleFilterModel filter);
    Task<RoleDetailsViewModel?> GetRoleByIdAsync(Guid id);
    Task AddRoleAsync(RoleEditModel model);
    Task UpdateRoleAsync(RoleEditModel model);
    Task DeleteRoleAsync(Guid id);
    Task<List<EndpointViewModel>> GetAvailableEndpointsAsync();
}
