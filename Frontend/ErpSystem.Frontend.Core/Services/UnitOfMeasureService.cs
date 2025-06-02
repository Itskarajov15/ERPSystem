using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.UnitsOfMeasure;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class UnitOfMeasureService : IUnitOfMeasureService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UnitOfMeasureService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PageResult<UnitOfMeasureViewModel>> GetUnitsOfMeasureAsync(int page = 1, int pageSize = 10)
    {
        var token = GetToken();
        var paginationParams = new PaginationParams { Page = page, PageSize = pageSize };
        var response = await _apiService.GetAsync<PageResult<UnitOfMeasureViewModel>>(
            $"/api/unitsOfMeasure/get-all?page={paginationParams.Page}&pageSize={paginationParams.PageSize}",
            token
        );
        return response ?? new PageResult<UnitOfMeasureViewModel>();
    }

    public async Task<UnitOfMeasureViewModel?> GetUnitOfMeasureByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<UnitOfMeasureViewModel>(
            $"/api/unitsOfMeasure/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> CreateUnitOfMeasureAsync(UnitOfMeasureEditModel model)
    {
        var token = GetToken();
        var response = await _apiService.PostAsync<Guid>("/api/unitsOfMeasure/add", model, token);
        return response;
    }

    public async Task UpdateUnitOfMeasureAsync(UnitOfMeasureEditModel model)
    {
        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/unitsOfMeasure/update/{model.Id}", model, token);
    }

    public async Task DeleteUnitOfMeasureAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/unitsOfMeasure/delete/{id}", token);
    }

    private string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
    }
} 