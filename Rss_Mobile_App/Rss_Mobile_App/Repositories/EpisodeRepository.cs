using Rss_Tracking_App.Models.Dto;
using System.Net.Http.Json;

namespace Rss_Mobile_App.Repositories
{
    public interface IEpisodeRepository : IBaseRepository<EpisodeDto>
    {
        Task<List<EpisodeDto>> GetEpisodesByFeedId(Guid feedId);
    }
    public class EpisodeRepository : BaseRepository<EpisodeDto>, IEpisodeRepository
    {
        public EpisodeRepository() : base("episodes") { }
        public async Task<List<EpisodeDto>> GetEpisodesByFeedId(Guid feedId)
        {
            HttpRequestMessage message = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{httpClient.BaseAddress.AbsoluteUri}/feed/{feedId}")
            };
            var result = await httpClient.SendAsync(message);
            result.EnsureSuccessStatusCode();
            return Deserialize<List<EpisodeDto>>(await result.Content.ReadAsStringAsync());
    }
}
}
