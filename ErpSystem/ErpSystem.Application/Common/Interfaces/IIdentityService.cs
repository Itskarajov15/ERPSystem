using ErpSystem.Application.Authentication.DTOs;
using ErpSystem.Application.Common.Models;

namespace ErpSystem.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<string> CreateUserAsync(
        string userName,
        string password,
        string email,
        string firstName,
        string lastName
    );

    Task<bool> IsInRoleAsync(Guid userId, string role);

    Task AddToRoleAsync(Guid userId, string role);

    Task DeleteUserAsync(Guid userId);

    Task<AuthenticationResult> AuthenticateAsync(string userName, string password);

    Task<RoleDto> CreateRoleAsync(string name, string description);

    Task DeleteRoleAsync(Guid id);

    Task<IEnumerable<RoleDto>> GetAllRolesAsync();

    Task RemoveRoleAsync(Guid userId, string roleName);
}
