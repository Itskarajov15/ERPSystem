using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.DTOs;
using ErpSystem.Frontend.Core.Models.Roles;
using ErpSystem.Frontend.Core.Models.Users;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class RoleService : IRoleService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ErrorTranslationService _errorTranslationService;

    public RoleService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ErrorTranslationService errorTranslationService
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<PageResult<RoleViewModel>> GetRolesAsync(RoleFilterModel filter)
    {
        var token = GetToken();
        var queryParams = $"?page={filter.Page}&pageSize={filter.PageSize}";

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            queryParams += $"&searchTerm={Uri.EscapeDataString(filter.SearchTerm)}";
        }

        var response = await _apiService.GetAsync<PageResult<RoleViewModel>>(
            $"/api/authentication/roles{queryParams}",
            token
        );

        return response ?? new PageResult<RoleViewModel>();
    }

    public async Task<RoleDetailsViewModel?> GetRoleByIdAsync(Guid id)
    {
        var token = GetToken();

        var roleDto = await _apiService.GetAsync<RoleDto>($"/api/authentication/roles/{id}", token);

        if (roleDto == null)
        {
            return null;
        }

        return new RoleDetailsViewModel
        {
            Id = Guid.Parse(roleDto.Id),
            Name = roleDto.Name,
            Description = roleDto.Description,
            CreatedAt = DateTime.Now,
            Permissions =
                roleDto
                    .Permissions?.Select(p => new PermissionViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Controller = p.ControllerName,
                        Action = p.ActionName,
                    })
                    .ToList() ?? new List<PermissionViewModel>(),
        };
    }

    public async Task AddRoleAsync(RoleEditModel model)
    {
        var token = GetToken();
        try
        {
            var createRoleDto = new CreateRoleDto
            {
                Name = model.Name,
                Description = model.Description,
                PermissionIds = model
                    .SelectedPermissionIds.Where(id => Guid.TryParse(id, out _))
                    .Select(Guid.Parse)
                    .ToList(),
            };

            await _apiService.PostAsync<object>("/api/authentication/roles", createRoleDto, token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task UpdateRoleAsync(RoleEditModel model)
    {
        var token = GetToken();
        try
        {
            var updateRoleDto = new UpdateRoleDto
            {
                Name = model.Name,
                Description = model.Description,
                PermissionIds = model
                    .SelectedPermissionIds.Where(id => Guid.TryParse(id, out _))
                    .Select(Guid.Parse)
                    .ToList(),
            };

            await _apiService.PutAsync<object>(
                $"/api/authentication/roles/{model.Id}",
                updateRoleDto,
                token
            );
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task DeleteRoleAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.DeleteAsync($"/api/authentication/roles/{id}", token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
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
