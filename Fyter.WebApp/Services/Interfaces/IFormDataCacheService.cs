namespace Fyter.WebApp.Services.Interfaces;

public interface IFormDataCacheService
{
    Task ClearFormDataAsync(string key);
    Task<T> LoadFormDataAsync<T>(string key);
    Task SaveFormDataAsync<T>(string key, T data);
}
