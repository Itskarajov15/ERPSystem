namespace ErpSystem.Frontend.Core.Interfaces;

public interface IApiService
{
    Task<T?> GetAsync<T>(string endpoint, string? token = null);
    Task<T?> PostAsync<T>(string endpoint, object data, string? token = null);
    Task<T?> PutAsync<T>(string endpoint, object data, string? token = null);
    Task DeleteAsync(string endpoint, string? token = null);
    Task<byte[]> GetBytesAsync(string endpoint, string? token = null);
}
