﻿using Rss_Tracking_App.Models.Dto;

namespace Rss_Mobile_App.Repositories
{
    public class FeedRepository : BaseRepository<FeedDto>
    {
        public FeedRepository() : base("feeds") { }
        public async Task<FeedDto> UpdateFeed(Guid feedId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/{feedId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<FeedDto>(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<FeedDto>> GetFeedByCreator(Guid creatorId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/author/{creatorId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync());
        }
        public async Task<List<FeedDto>> GetFeedsByUserId(Guid userId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/user/{userId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<List<FeedDto>>(await result.Content.ReadAsStringAsync());
        }
    }
}
