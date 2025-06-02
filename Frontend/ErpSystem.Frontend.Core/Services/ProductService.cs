using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Products;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class ProductService : IProductService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProductService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PageResult<ProductViewModel>> GetProductsAsync(ProductFilterModel? filter = null)
    {
        var token = GetToken();
        var endpoint = "/api/products/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                queryParams.Add($"SearchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
            }
            if (filter.OnlyLowStock.HasValue)
            {
                queryParams.Add($"OnlyLowStock={filter.OnlyLowStock.Value}");
            }
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<ProductViewModel>>(endpoint, token);
        return response ?? new PageResult<ProductViewModel>();
    }

    public async Task<ProductViewModel?> GetProductByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<ProductViewModel>($"/api/products/get-by-id/{id}", token);
    }

    public async Task<Guid> AddProductAsync(ProductEditModel model)
    {
        var token = GetToken();
        var response = await _apiService.PostAsync<Guid>("/api/products/add", model, token);
        return response;
    }

    public async Task UpdateProductAsync(ProductEditModel model)
    {
        if (model.Id == null)
        {
            throw new ArgumentException("Product ID cannot be null when updating");
        }

        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/products/update/{model.Id}", model, token);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/products/delete/{id}", token);
    }

    private string? GetToken() => _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
