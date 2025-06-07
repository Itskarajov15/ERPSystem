using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Suppliers;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class SupplierService : ISupplierService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SupplierService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PageResult<SupplierViewModel>> GetSuppliersAsync(
        SupplierFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/suppliers/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                queryParams.Add($"SearchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
            }
            if (filter.IsActive.HasValue)
            {
                queryParams.Add($"IsActive={filter.IsActive.Value}");
            }
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<SupplierViewModel>>(endpoint, token);
        return response ?? new PageResult<SupplierViewModel>();
    }

    public async Task<SupplierViewModel?> GetSupplierByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<SupplierViewModel>(
            $"/api/suppliers/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> AddSupplierAsync(SupplierEditModel model)
    {
        var token = GetToken();
        var response = await _apiService.PostAsync<Guid>("/api/suppliers/add", model, token);
        return response;
    }

    public async Task UpdateSupplierAsync(SupplierEditModel model)
    {
        if (model.Id == null)
        {
            throw new ArgumentException("Supplier ID cannot be null when updating");
        }

        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/suppliers/update/{model.Id}", model, token);
    }

    public async Task DeleteSupplierAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/suppliers/delete/{id}", token);
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
