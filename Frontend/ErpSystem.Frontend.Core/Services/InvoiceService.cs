using ErpSystem.Frontend.Core.Interfaces;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Core.Models.Invoices;
using Microsoft.AspNetCore.Http;

namespace ErpSystem.Frontend.Core.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ErrorTranslationService _errorTranslationService;

    public InvoiceService(
        IApiService apiService,
        IHttpContextAccessor httpContextAccessor,
        ErrorTranslationService errorTranslationService
    )
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
        _errorTranslationService = errorTranslationService;
    }

    public async Task<PageResult<InvoiceViewModel>> GetInvoicesAsync(
        InvoiceFilterModel? filter = null
    )
    {
        var token = GetToken();
        var endpoint = "/api/invoices/get-all";

        if (filter != null)
        {
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                queryParams.Add($"SearchTerm={Uri.EscapeDataString(filter.SearchTerm)}");
            }
            if (filter.Status.HasValue)
            {
                queryParams.Add($"Status={filter.Status.Value}");
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

        var response = await _apiService.GetAsync<PageResult<InvoiceViewModel>>(endpoint, token);
        return response ?? new PageResult<InvoiceViewModel>();
    }

    public async Task<InvoiceDetailViewModel?> GetInvoiceByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<InvoiceDetailViewModel>(
            $"/api/invoices/get-by-id/{id}",
            token
        );
    }

    public async Task<Guid> CreateInvoiceFromOrderAsync(Guid orderId, string? notes = null)
    {
        var token = GetToken();

        var request = new CreateInvoiceFromOrderRequest { OrderId = orderId, Notes = notes };

        try
        {
            var response = await _apiService.PostAsync<Guid>(
                "/api/invoices/create-from-order",
                request,
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

    public async Task UpdateInvoiceStatusAsync(Guid invoiceId, InvoiceStatus status)
    {
        var token = GetToken();

        var request = new UpdateInvoiceStatusRequest { InvoiceId = invoiceId, Status = status };

        try
        {
            await _apiService.PutAsync<object>(
                $"/api/invoices/{invoiceId}/update-status",
                request,
                token
            );
        }
        catch (Exception ex)
        {
            var translatedMessage = _errorTranslationService.Translate(ex.Message);
            throw new Exception(translatedMessage);
        }
    }

    public async Task<byte[]> GetInvoicePdfAsync(Guid invoiceId)
    {
        var token = GetToken();
        return await _apiService.GetBytesAsync($"/api/invoices/{invoiceId}/pdf", token);
    }

    private string? GetToken() =>
        _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
}
