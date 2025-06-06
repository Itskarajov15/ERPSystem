using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.PaymentMethods;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaymentMethodService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PageResult<PaymentMethodViewModel>> GetPaymentMethodsAsync(
        PaymentMethodFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/payment-methods/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<PaymentMethodViewModel>>(
            endpoint,
            token
        );
        return response ?? new PageResult<PaymentMethodViewModel>();
    }

    public async Task<List<PaymentMethodViewModel>> GetAllPaymentMethodsAsync()
    {
        var token = GetToken();
        var response = await _apiService.GetAsync<List<PaymentMethodViewModel>>(
            "/api/payment-methods/get-dropdown-list",
            token
        );
        return response ?? new List<PaymentMethodViewModel>();
    }

    public async Task<PaymentMethodViewModel?> GetPaymentMethodByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<PaymentMethodViewModel>(
            $"/api/payment-methods/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> AddPaymentMethodAsync(PaymentMethodEditModel model)
    {
        var token = GetToken();

        var apiModel = new { Name = model.Name };

        var response = await _apiService.PostAsync<Guid>(
            "/api/payment-methods/add",
            apiModel,
            token
        );
        return response;
    }

    public async Task UpdatePaymentMethodAsync(PaymentMethodEditModel model)
    {
        if (model.Id == null)
        {
            throw new ArgumentException("Payment method ID cannot be null when updating");
        }

        var token = GetToken();

        var apiModel = new { Id = model.Id.Value, Name = model.Name };

        await _apiService.PutAsync<object>(
            $"/api/payment-methods/update/{model.Id}",
            apiModel,
            token
        );
    }

    public async Task DeletePaymentMethodAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/payment-methods/delete/{id}", token);
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
