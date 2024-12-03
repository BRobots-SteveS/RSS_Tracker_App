using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public readonly HttpClient httpClient;
        public const string BaseUrl = "https://0fl257js-5009.euw.devtunnels.ms/api/v1.0/";
        public BaseRepository(string route)
        {
            httpClient = new()
            {
                BaseAddress = new Uri($"{BaseUrl}{route}")
            };
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<List<T>> GetAllRows()
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}")
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Table does not exist");
            return System.Text.Json.JsonSerializer.Deserialize<List<T>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<T> GetRowById(Guid id)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}?id={id}")
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Record not found for id: {id}");
            return System.Text.Json.JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> CreateRow(T item)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}"),
                Content = JsonContent.Create(item)
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Failed to create item");
            return System.Text.Json.JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());
        }

        public async Task<T> UpdateRow(T item, Guid itemId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{itemId}"),
                Content = JsonContent.Create(item)
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Record not updated for id: {itemId}");
            return System.Text.Json.JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());

        }
    }
}
