using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Payments;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class PaymentService : IPaymentService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ErrorTranslationService _errorTranslationService;

    public PaymentService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ErrorTranslationService errorTranslationService
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<PageResult<PaymentViewModel>> GetPaymentsAsync(
        PaymentFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/payments/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.InvoiceNumber))
            {
                queryParams.Add($"InvoiceNumber={Uri.EscapeDataString(filter.InvoiceNumber)}");
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

        var response = await _apiService.GetAsync<PageResult<PaymentViewModel>>(endpoint, token);
        return response ?? new PageResult<PaymentViewModel>();
    }

    public async Task<PaymentDetailViewModel?> GetPaymentByIdAsync(Guid paymentId)
    {
        var token = GetToken();
        return await _apiService.GetAsync<PaymentDetailViewModel>(
            $"/api/payments/get-by-id/{paymentId}",
            token
        );
    }

    public async Task RecordPaymentAsync(RecordPaymentRequest request)
    {
        var token = GetToken();

        try
        {
            await _apiService.PostAsync<object>("/api/payments/record", request, token);
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
