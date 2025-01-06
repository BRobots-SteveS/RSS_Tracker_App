using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public interface IFeedRepository : IBaseRepository<FeedDto>
    {
        Task<FeedDto> UpdateFeed(Guid feedId);
        Task<List<FeedDto>> GetFeedByCreator(Guid creatorId);
        Task<List<FeedDto>> GetFeedsByUserId(Guid userId);
        Task<List<FeedDto>> GetFeedsByFilter(string title, string creatorId, string description, string authorName, string platform);
    }
    public class FeedRepository : BaseRepository<FeedDto>, IFeedRepository
    {
        public FeedRepository() : base("feeds") { }
        public async Task<FeedDto> UpdateFeed(Guid feedId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/{feedId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<FeedDto>(await result.Content.ReadAsStringAsync()) ?? new();
        }
        public async Task<List<FeedDto>> GetFeedByCreator(Guid creatorId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/author/{creatorId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync()) ?? new();
        }
        public async Task<List<FeedDto>> GetFeedsByUserId(Guid userId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/user/{userId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync()) ?? new();
        }
        public async Task<List<FeedDto>> GetFeedsByFilter(string title, string creatorId, string description, string authorName, string platform)
        {
            string querystring = (string.IsNullOrEmpty(title)? string.Empty : $"title={title}&") +
                (string.IsNullOrEmpty(description) ? string.Empty : $"description={description}&") +
                (string.IsNullOrEmpty(creatorId) ? string.Empty : $"creatorid={creatorId}&") +
                (string.IsNullOrEmpty(authorName) ? string.Empty : $"authorname={authorName}&") +
                (string.IsNullOrEmpty(platform) ? string.Empty : $"platform={platform}&");
            if (string.IsNullOrEmpty(querystring)) return new();
            querystring.TrimEnd('&');
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}/filter?{querystring}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync()) ?? new();
        }

        public override async Task<FeedDto> CreateRow(FeedDto item)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{httpClient.BaseAddress!.AbsoluteUri}"),
                Content = JsonContent.Create(item)
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync())?.FirstOrDefault() ?? new();
        }
    }
}
