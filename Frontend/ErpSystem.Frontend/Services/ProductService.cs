using ErpSystem.Frontend.Web.Models.Common;
using ErpSystem.Frontend.Web.Models.Products;

namespace ErpSystem.Frontend.Web.Services;

public class ProductService : IProductService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PagedResponse<ProductViewModel>> GetProductsAsync()
    {
        var token = _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
        var response = await _apiService.GetAsync<PagedResponse<ProductViewModel>>("/api/products/get-all", token);
        return response ?? new PagedResponse<ProductViewModel>();
    }
} 