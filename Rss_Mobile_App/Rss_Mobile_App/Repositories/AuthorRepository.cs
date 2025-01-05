using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public interface IAuthorRepository : IBaseRepository<AuthorDto>
    {
        Task<List<AuthorDto>> GetAuthorsByFeedId(Guid feedId);
        Task<List<AuthorDto>> GetAuthorsByNameAndEmail(string name, string email);
    }
    public class AuthorRepository : BaseRepository<AuthorDto>, IAuthorRepository
    {
        public AuthorRepository() : base("authors") { }
        public async Task<List<AuthorDto>> GetAuthorsByFeedId(Guid feedId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/feed/{feedId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<AuthorDto>>(await result.Content.ReadAsStringAsync()) ?? new();
        }

        public async Task<List<AuthorDto>> GetAuthorsByNameAndEmail(string name, string email)
        {
            string querystring = (string.IsNullOrEmpty(name) ? string.Empty : $"authorname={name}&") +
                                 (string.IsNullOrEmpty(email) ? string.Empty : $"email={email}");
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/filter?{querystring}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<AuthorDto>>(await result.Content.ReadAsStringAsync()) ?? new();
        }
    }
}
