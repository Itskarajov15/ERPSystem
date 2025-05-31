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

    public async Task<PageResult<SupplierViewModel>> GetSuppliersAsync()
    {
        var token = _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
        var response = await _apiService.GetAsync<PageResult<SupplierViewModel>>(
            "/api/suppliers/get-all",
            token
        );
        return response ?? new PageResult<SupplierViewModel>();
    }
}
