using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public class UserRepository : BaseRepository<UserDto>
    {
        public UserRepository() : base("user") { }
        public async Task<UserDto> DoLogin(string username, string password)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}")
            };
            message.Headers.Add("authorization", $"Basic {username}:{Convert.ToHexString(System.Text.Encoding.UTF8.GetBytes(password))}");
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Table does not exist");
            return System.Text.Json.JsonSerializer.Deserialize<UserDto>(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<UserFavoriteDto>> GetFavoritesByUserId(Guid userId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/favorite/{userId}")
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Table does not exist");
            return System.Text.Json.JsonSerializer.Deserialize<List<UserFavoriteDto>>(await result.Content.ReadAsStringAsync());
        }

        public async Task<UserFavoriteDto> CreateFavorite(UserFavoriteDto favoriteDto)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/favorite"),
                Content = JsonContent.Create(favoriteDto)
            };
            var result = await httpClient.SendAsync(message);
            if (result == null || !result.IsSuccessStatusCode) throw new Exception($"Table does not exist");
            return System.Text.Json.JsonSerializer.Deserialize<UserFavoriteDto>(await result.Content.ReadAsStringAsync());
        }
    }
}
