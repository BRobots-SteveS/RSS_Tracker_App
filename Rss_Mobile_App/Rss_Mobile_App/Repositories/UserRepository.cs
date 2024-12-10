using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public interface IUserRepository : IBaseRepository<UserDto>
    {
        Task<UserDto> DoLogin(string username, string password);
        Task<UserDto> DoRegister(string username, string password);
        Task<List<UserFavoriteDto>> GetFavoritesByUserId(Guid userId);
        Task<UserFavoriteDto> CreateFavorite(Guid userId, Guid? feedId = null, Guid? authorId = null);
    }
    public class UserRepository : BaseRepository<UserDto>, IUserRepository
    {
        public UserRepository() : base("users") { }
        public async Task<UserDto> DoLogin(string username, string password)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/login")
            };
            message.Headers.Add("authorization", $"Basic {username}:{Convert.ToHexString(System.Text.Encoding.UTF8.GetBytes(password))}");
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<UserDto>(await result.Content.ReadAsStringAsync());
        }
        public async Task<UserDto> DoRegister(string username, string password)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}")
            };
            message.Headers.Add("authorization", $"Basic {username}:{Convert.ToHexString(System.Text.Encoding.UTF8.GetBytes(password))}");
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            return Deserialize<UserDto>(content);
        }
        public async Task<List<UserFavoriteDto>> GetFavoritesByUserId(Guid userId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/favorite/{userId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<UserFavoriteDto>>(await result.Content.ReadAsStringAsync());
        }

        public async Task<UserFavoriteDto> CreateFavorite(Guid userId, Guid? feedId = null, Guid? authorId = null)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/favorite?userid={userId}{(feedId.HasValue? $"&feedid={feedId.Value}" : "")}{(authorId.HasValue ? $"&feedid={authorId.Value}" : "")}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<UserFavoriteDto>(await result.Content.ReadAsStringAsync());
        }
    }
}
