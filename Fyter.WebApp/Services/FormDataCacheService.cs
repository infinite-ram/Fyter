using System.Text.Json;
using Microsoft.JSInterop;
using Fyter.WebApp.Services.Interfaces;
using Fyter.WebApp.Utilities;

namespace Fyter.WebApp.Services;

public class FormDataCacheService : IFormDataCacheService
{
    private readonly IJSRuntime _jsRuntime;

    public FormDataCacheService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveFormDataAsync<T>(string key, T data)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
        };

        var jsonData = JsonSerializer.Serialize(data, options);
        await _jsRuntime.InvokeVoidAsync("formCache.saveData", key, jsonData);
    }

    public async Task<T> LoadFormDataAsync<T>(string key)
    {
        var jsonData = await _jsRuntime.InvokeAsync<string>("formCache.loadData", key);
        if (!string.IsNullOrEmpty(jsonData))
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
            };

            return JsonSerializer.Deserialize<T>(jsonData, options);
        }

        return default;
    }

    public async Task ClearFormDataAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("formCache.clearData", key);
    }
}
