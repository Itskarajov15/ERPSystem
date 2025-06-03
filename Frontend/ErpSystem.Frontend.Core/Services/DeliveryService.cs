using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Deliveries;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ErrorTranslationService _errorTranslationService;

    public DeliveryService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ErrorTranslationService errorTranslationService
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<PageResult<DeliveryViewModel>> GetDeliveriesAsync(
        DeliveryFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/deliveries/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (filter.SupplierId.HasValue)
            {
                queryParams.Add($"SupplierId={filter.SupplierId.Value}");
            }
            if (filter.FromDate.HasValue)
            {
                queryParams.Add($"FromDate={filter.FromDate.Value:yyyy-MM-dd}");
            }
            if (filter.ToDate.HasValue)
            {
                queryParams.Add($"ToDate={filter.ToDate.Value:yyyy-MM-dd}");
            }
            if (filter.Status.HasValue)
            {
                queryParams.Add($"Status={filter.Status.Value}");
            }
            queryParams.Add($"Page={filter.Page}");
            queryParams.Add($"PageSize={filter.PageSize}");

            if (queryParams.Any())
            {
                endpoint += "?" + string.Join("&", queryParams);
            }
        }

        var response = await _apiService.GetAsync<PageResult<DeliveryViewModel>>(endpoint, token);
        return response ?? new PageResult<DeliveryViewModel>();
    }

    public async Task<DeliveryDetailViewModel?> GetDeliveryByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<DeliveryDetailViewModel>(
            $"/api/deliveries/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> CreateDeliveryAsync(DeliveryCreateModel model)
    {
        var token = GetToken();

        var apiRequest = new DeliveryApiRequest
        {
            SupplierId = model.SupplierId,
            DeliveryNumber = model.DeliveryNumber,
            DeliveryDate = model.DeliveryDate,
            Comment = model.Comment,
            Items = model
                .Items.Select(item => new DeliveryItemApiRequest
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                })
                .ToList(),
        };

        try
        {
            var response = await _apiService.PostAsync<Guid>(
                "/api/deliveries/add",
                apiRequest,
                token
            );
            return response;
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task StartDeliveryProgressAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/deliveries/{id}/start-progress", null, token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task CompleteDeliveryAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/deliveries/{id}/complete", null, token);
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task DeleteDeliveryAsync(Guid id)
    {
        var token = GetToken();
        try
        {
            await _apiService.DeleteAsync($"/api/deliveries/delete/{id}", token);
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
