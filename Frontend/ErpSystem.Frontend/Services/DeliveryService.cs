using ErpSystem.Frontend.Web.Models.Deliveries;

namespace ErpSystem.Frontend.Web.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IApiService _apiService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DeliveryService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
    {
        _apiService = apiService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<(List<DeliveryViewModel> Items, int TotalCount)> GetDeliveriesAsync(DeliveryFilterModel filter)
    {
        var token = GetToken();
        var queryString = BuildQueryString(filter);
        var response = await _apiService.GetAsync<PageResult>($"/api/deliveries/get-all{queryString}", token);

        return response != null 
            ? (response.Items, response.TotalCount) 
            : (new List<DeliveryViewModel>(), 0);
    }

    public async Task<DeliveryViewModel?> GetDeliveryByIdAsync(Guid id)
    {
        var token = GetToken();
        return await _apiService.GetAsync<DeliveryViewModel>($"/api/deliveries/get-by-id/{id}", token);
    }

    public async Task<Guid> CreateDeliveryAsync(DeliveryEditModel model)
    {
        var token = GetToken();
        var response = await _apiService.PostAsync<Guid>("/api/deliveries/add", model, token);
        return response;
    }

    public async Task CompleteDeliveryAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/deliveries/{id}/complete", null, token);
    }

    public async Task DeleteDeliveryAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.DeleteAsync($"/api/deliveries/delete/{id}", token);
    }

    public async Task<bool> UpdateDeliveryStatusAsync(int id, string status)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/deliveries/{id}/status", new { status }, token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateDeliveryCommentAsync(int id, string comment)
    {
        var token = GetToken();
        try
        {
            await _apiService.PutAsync<object>($"/api/deliveries/{id}/comment", new { comment }, token);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task ExportDeliveriesAsync(DeliveryFilterModel filter)
    {
        var token = GetToken();
        var queryString = BuildQueryString(filter);
        await _apiService.GetAsync<object>($"/api/deliveries/export{queryString}", token);
    }

    public async Task ImportPrioritiesAsync(IFormFile file)
    {
        var token = GetToken();
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(file.OpenReadStream());
        content.Add(streamContent, "file", file.FileName);
        
        await _apiService.PostAsync<object>("/api/deliveries/import-priorities", content, token);
    }

    public async Task StartDeliveryProgressAsync(Guid id)
    {
        var token = GetToken();
        await _apiService.PutAsync<object>($"/api/deliveries/{id}/start-progress", null, token);
    }

    private string? GetToken()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst("jwt_token")?.Value;
    }

    private string BuildQueryString(DeliveryFilterModel filter)
    {
        var query = new List<string>();

        if (filter.SupplierId.HasValue)
            query.Add($"supplierId={filter.SupplierId}");
        
        if (filter.FromDate.HasValue)
            query.Add($"fromDate={filter.FromDate.Value:yyyy-MM-dd}");
        
        if (filter.ToDate.HasValue)
            query.Add($"toDate={filter.ToDate.Value:yyyy-MM-dd}");
        
        if (filter.Status.HasValue)
            query.Add($"status={filter.Status.Value}");
        
        query.Add($"pageNumber={filter.Page}");
        query.Add($"pageSize={filter.PageSize}");

        return query.Any() ? "?" + string.Join("&", query) : string.Empty;
    }

    private class PageResult
    {
        public List<DeliveryViewModel> Items { get; set; } = new();
        public int TotalCount { get; set; }
    }
} 