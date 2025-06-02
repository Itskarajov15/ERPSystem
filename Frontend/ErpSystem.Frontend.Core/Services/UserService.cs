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

    public async Task<UserViewModel?> GetUserByIdAsync(string id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<UserViewModel>($"/api/users/get-by-id/{id}", token);
    }

    public async Task<PageResult<UserViewModel>> GetUsersAsync(int page = 1, int pageSize = 10)
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<PageResult<UserViewModel>>(
            $"/api/authentication/users?page={page}&pageSize={pageSize}",
            token
        );
        return response ?? new PageResult<UserViewModel>();
    }

    public async Task<PageResult<RoleViewModel>> GetRolesAsync()
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<PageResult<RoleViewModel>>(
            "/api/authentication/roles",
            token
        );
        return response ?? new PageResult<RoleViewModel>();
    }

    public async Task<bool> RegisterUserAsync(CreateUserViewModel model)
    {
        var token = GetToken();
        try
        {
            await _apiService.PostAsync<object>("/api/authentication/register", model, token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AssignRoleAsync(string userId, string roleName)
    {
        var token = GetToken();
        try
        {
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
        var token = GetToken();
        try
        {
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
        var token = GetToken();
        try
        {
            await _apiService.PostAsync<object>(
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

    public async Task<bool> EditRoleAsync(
        string roleId,
        string name,
        string description,
        List<string> permissionIds
    )
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>(
                $"/api/authentication/roles/{roleId}",
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
        var token = GetToken();
        try
        {
            await _apiService.DeleteAsync($"/api/authentication/roles/{roleId}", token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<List<EndpointViewModel>> GetAvailableEndpointsAsync()
    {
        var token = GetToken();
        try
        {
            var response = await _apiService.GetAsync<List<EndpointViewModel>>(
                "/api/authentication/endpoints",
                token
            );
            return response ?? new List<EndpointViewModel>();
        }
        catch
        {
            return new List<EndpointViewModel>();
        }
    }

    private string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
    }
}
