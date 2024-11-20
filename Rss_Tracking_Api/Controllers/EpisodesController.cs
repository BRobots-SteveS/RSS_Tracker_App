using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Repositories;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion(1.0f)]
    [Route("api/v[version:apiVersion]/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeRepository _episodes;
        private readonly IAuthorRepository _authors;
        private readonly ILogger<EpisodesController> _logger;
        public EpisodesController(ILogger<EpisodesController> logger, IEpisodeRepository episodes, IAuthorRepository authors)
        {
            _logger = logger;
            _episodes = episodes;
            _authors = authors;
        }

        [HttpGet]
        public async Task<ActionResult<List<EpisodeDto>>> GetAllEpisodes()
        {
            return Ok(_episodes.GetAllEpisodes().Select(x => DbMapper.EpisodeToDto(x, _authors.GetAuthorsByFeedId(x.FeedId).First())).ToList());
        }

        [HttpGet("/feed/{feedId}")]
        public async Task<ActionResult<List<EpisodeDto>>> GetEpisodeByFeedId(Guid feedId)
        {
            return Ok(_episodes.GetEpisodesByFeedId(feedId).ToList());
        }
    }
}
