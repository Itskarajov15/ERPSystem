using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Users;

namespace ErpSystem.Frontend.Core.Interfaces;

public interface IUserService
{
    Task<PageResult<UserViewModel>> GetUsersAsync(
        int page = 1,
        int pageSize = 25,
        string searchTerm = ""
    );

    Task<PageResult<RoleViewModel>> GetRolesAsync();

    Task<bool> RegisterUserAsync(CreateUserViewModel model);

    Task<bool> AssignRoleAsync(string userId, string roleName);

    Task<bool> RemoveRoleAsync(string userId, string roleName);

    Task<bool> CreateRoleAsync(string name, string description, List<string> permissionIds);

    Task<bool> EditRoleAsync(
        string roleId,
        string name,
        string description,
        List<string> permissionIds
    );

    Task<bool> DeleteRoleAsync(string roleId);

    Task<List<EndpointViewModel>> GetAvailableEndpointsAsync();
}
