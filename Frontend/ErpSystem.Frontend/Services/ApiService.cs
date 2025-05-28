using System.Net.Http.Headers;
using System.Text;
using ErpSystem.Frontend.Web.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ErpSystem.Frontend.Web.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public ApiService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings.Value;
        _httpClient.BaseAddress = new Uri(_apiSettings.BaseUrl);
    }

    public async Task<T?> GetAsync<T>(string endpoint, string? token = null)
    {
        SetAuthorizationHeader(token);

        var response = await _httpClient.GetAsync(endpoint);
        return await HandleResponse<T>(response);
    }

    public async Task<T?> PostAsync<T>(string endpoint, object data, string? token = null)
    {
        SetAuthorizationHeader(token);

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        return await HandleResponse<T>(response);
    }

    public async Task<T?> PutAsync<T>(string endpoint, object data, string? token = null)
    {
        SetAuthorizationHeader(token);

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(endpoint, content);
        return await HandleResponse<T>(response);
    }

    public async Task DeleteAsync(string endpoint, string? token = null)
    {
        SetAuthorizationHeader(token);

        var response = await _httpClient.DeleteAsync(endpoint);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
        }
    }

    private void SetAuthorizationHeader(string? token)
    {
        _httpClient.DefaultRequestHeaders.Authorization = token != null
            ? new AuthenticationHeaderValue("Bearer", token)
            : null;
    }

    private async Task<T?> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error: {response.StatusCode} - {content}");
        }

        return JsonConvert.DeserializeObject<T>(content);
    }
} 