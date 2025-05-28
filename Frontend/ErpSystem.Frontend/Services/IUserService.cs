using ErpSystem.Frontend.Web.Models.Common;
using ErpSystem.Frontend.Web.Models.Users;

namespace ErpSystem.Frontend.Web.Services;

public interface IUserService
{
    Task<bool> CreateUserAsync(CreateUserViewModel model);
    Task<PagedResponse<UserViewModel>> GetUsersAsync(int page = 1, int pageSize = 10);
    Task<List<RoleViewModel>> GetAvailableRolesAsync(int page = 1, int pageSize = 100);
    Task<bool> AssignRoleAsync(string userId, string roleName);
    Task<bool> RemoveRoleAsync(string userId, string roleName);
    Task<bool> CreateRoleAsync(string name, string description, List<string> permissionIds);
    Task<bool> DeleteRoleAsync(string roleId);
    Task<List<EndpointViewModel>> GetAvailableEndpointsAsync();
} 