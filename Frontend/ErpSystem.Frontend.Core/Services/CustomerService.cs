using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Customers;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class CustomerService : ICustomerService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomerService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PageResult<CustomerViewModel>> GetCustomersAsync(
        CustomerFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/customers/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                queryParams.Add($"SearchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
            }
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<CustomerViewModel>>(endpoint, token);
        return response ?? new PageResult<CustomerViewModel>();
    }

    public async Task<CustomerViewModel?> GetCustomerByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<CustomerViewModel>(
            $"/api/customers/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> AddCustomerAsync(CustomerEditModel model)
    {
        var token = GetToken();
        var response = await _apiService.PostAsync<Guid>("/api/customers/add", model, token);
        return response;
    }

    public async Task UpdateCustomerAsync(CustomerEditModel model)
    {
        if (model.Id == null)
        {
            throw new ArgumentException("Customer ID cannot be null when updating");
        }

        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/customers/update/{model.Id}", model, token);
    }

    public async Task DeleteCustomerAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/customers/delete/{id}", token);
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
