namespace DongHoBlazorApp.Web;

public class ApiClient(HttpClient httpClient)
{
    public Task<T> GetFromJsonAsync<T>(string path)
    {
        return httpClient.GetFromJsonAsync<T>(path)!;
    }

    public async Task<TResponse> PostAsJsonAsync<TResponse, TValue>(string path, TValue value)
    {
        var response = await httpClient.PostAsJsonAsync(path, value);
        if (response.IsSuccessStatusCode)
        {
             return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        // Handle error or return default
        return default!;
    }

    public async Task<TResponse> PutAsJsonAsync<TResponse, TValue>(string path, TValue value)
    {
        var response = await httpClient.PutAsJsonAsync(path, value);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        // Handle error or return default
        return default!;
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string path)
    {
        var response = await httpClient.DeleteAsync(path);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
        // Handle error or return default
        return default!;
    }
}
