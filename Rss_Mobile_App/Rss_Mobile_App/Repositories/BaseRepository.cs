using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllRows();
        Task<T> GetRowById(Guid id);
        Task<T> CreateRow(T item);
        Task<T> UpdateRow(T item, Guid itemId);
        Task DeleteRow(T item, Guid itemId);
    }
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal readonly HttpClient httpClient;
        internal const string BaseUrl = "https://0fl257js-5009.euw.devtunnels.ms/api/v1.0/";
        internal readonly string Route;
        internal readonly System.Text.Json.JsonSerializerOptions options = new()
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            AllowTrailingCommas = true
        };
        public BaseRepository(string route)
        {
            Route = route;
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
            result.EnsureSuccessStatusCode();
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
            if (result.StatusCode == System.Net.HttpStatusCode.NotFound) return (T)Activator.CreateInstance(typeof(T));
            result.EnsureSuccessStatusCode();
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
            result.EnsureSuccessStatusCode();
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
            result.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<T>(await result.Content.ReadAsStringAsync());
        }

        public async Task DeleteRow(T item, Guid itemId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}?{Route}id={itemId}"),
                Content = JsonContent.Create(item)
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return;
        }
        internal TItem? Deserialize<TItem>(string content) where TItem : class
        {
            return System.Text.Json.JsonSerializer.Deserialize<TItem>(content, options);
        }
    }
}
