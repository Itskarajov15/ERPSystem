using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Orders;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class OrderService : IOrderService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ErrorTranslationService _errorTranslationService;

    public OrderService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ErrorTranslationService errorTranslationService
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<PageResult<OrderViewModel>> GetOrdersAsync(OrderFilterModel? filter = null)
    {
        var token = GetToken();
        var endpoint = "/api/orders/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.Status))
            {
                queryParams.Add($"Status={Uri.EscapeDataString(filter.Status)}");
            }
            if (filter.CustomerId.HasValue)
            {
                queryParams.Add($"CustomerId={filter.CustomerId.Value}");
            }
            if (filter.FromDate.HasValue)
            {
                queryParams.Add($"FromDate={filter.FromDate.Value:yyyy-MM-dd}");
            }
            if (filter.ToDate.HasValue)
            {
                queryParams.Add($"ToDate={filter.ToDate.Value:yyyy-MM-dd}");
            }
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<OrderViewModel>>(endpoint, token);
        return response ?? new PageResult<OrderViewModel>();
    }

    public async Task<OrderDetailViewModel?> GetOrderByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<OrderDetailViewModel>(
            $"/api/orders/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> AddOrderAsync(OrderCreateModel model)
    {
        var token = GetToken();

        var apiRequest = new OrderApiRequest
        {
            CustomerId = model.CustomerId,
            PaymentMethodId = model.PaymentMethodId,
            Notes = model.Notes,
            Items = model
                .Items.Select(item => new OrderItemApiRequest
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                })
                .ToList(),
        };

        try
        {
            var response = await _apiService.PostAsync<Guid>("/api/orders/add", apiRequest, token);
            return response;
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task CompleteOrderAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/orders/{id}/complete", null, token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task CancelOrderAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/orders/{id}/cancel", null, token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.DeleteAsync($"/api/orders/delete/{id}", token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
