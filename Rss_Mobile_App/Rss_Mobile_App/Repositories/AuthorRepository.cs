using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public class AuthorRepository : BaseRepository<AuthorDto>
    {
        public AuthorRepository() : base("author") { }
        public async Task<List<AuthorDto>> GetAuthorsByFeedId(Guid feedId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/feed/{feedId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<List<AuthorDto>>(await result.Content.ReadAsStringAsync());
        }

    }
}
