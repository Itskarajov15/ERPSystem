using ErpSystem.Frontend.Web.Models.Common;
using ErpSystem.Frontend.Web.Models.Suppliers;

namespace ErpSystem.Frontend.Web.Services;

public class SupplierService : ISupplierService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SupplierService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PagedResponse<SupplierViewModel>> GetSuppliersAsync()
    {
        var token = _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
        var response = await _apiService.GetAsync<PagedResponse<SupplierViewModel>>("/api/suppliers/get-all", token);
        return response ?? new PagedResponse<SupplierViewModel>();
    }
} 