using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Users;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class UserService : IUserService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> CreateUserAsync(CreateUserViewModel model)
    {
        try
        {
            var token = GetToken();
            var userId = await _apiService.PostAsync<string>(
                "/api/authentication/register",
                new
                {
                    model.Username,
                    model.Password,
                    model.Email,
                    model.FirstName,
                    model.LastName,
                },
                token
            );

            if (string.IsNullOrEmpty(userId))
                return false;

            // Assign selected roles
            foreach (var roleName in model.SelectedRoles)
            {
                await AssignRoleAsync(userId, roleName);
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<PagedResponse<UserViewModel>> GetUsersAsync(int page = 1, int pageSize = 10)
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<PagedResponse<UserViewModel>>(
            $"/api/authentication/users?page={page}&pageSize={pageSize}",
            token
        );
        return response ?? new PagedResponse<UserViewModel>();
    }

    public async Task<List<RoleViewModel>> GetAvailableRolesAsync(int page = 1, int pageSize = 100)
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<PagedResponse<RoleViewModel>>(
            $"/api/authentication/roles?page={page}&pageSize={pageSize}",
            token
        );
        return response?.Items ?? new List<RoleViewModel>();
    }

    public async Task<List<EndpointViewModel>> GetAvailableEndpointsAsync()
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<List<EndpointViewModel>>(
            "/api/authentication/endpoints",
            token
        );
        return response ?? new List<EndpointViewModel>();
    }

    public async Task<bool> AssignRoleAsync(string userId, string roleName)
    {
        try
        {
            var token = GetToken();
            await _apiService.PostAsync<object>(
                $"/api/authentication/users/{userId}/roles",
                roleName,
                token
            );
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RemoveRoleAsync(string userId, string roleName)
    {
        try
        {
            var token = GetToken();
            await _apiService.DeleteAsync(
                $"/api/authentication/users/{userId}/roles/{roleName}",
                token
            );
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> CreateRoleAsync(
        string name,
        string description,
        List<string> permissionIds
    )
    {
        try
        {
            var token = GetToken();
            await _apiService.PostAsync<string>(
                "/api/authentication/roles",
                new
                {
                    name,
                    description,
                    permissionIds,
                },
                token
            );
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteRoleAsync(string roleId)
    {
        try
        {
            var token = GetToken();
            await _apiService.DeleteAsync($"/api/authentication/roles/{roleId}", token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
