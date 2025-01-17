﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Repositories;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<EpisodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEpisodes()
        {
            var episodes = _episodes.GetAll().Select(DbMapper.EpisodeToDto).ToList();
            episodes.Sort((x, y) => DateTime.Compare(y.CreatedOn.UtcDateTime, x.CreatedOn.UtcDateTime));
            return new OkObjectResult(episodes);
        }

        [HttpGet("feed/{feedId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<EpisodeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEpisodesByFeedId(Guid feedId)
        {
            var episodes = _episodes.GetEpisodesByFeedId(feedId).Select(DbMapper.EpisodeToDto).ToList();
            episodes.Sort((x, y) => DateTime.Compare(y.CreatedOn.UtcDateTime, x.CreatedOn.UtcDateTime));
            return new OkObjectResult(episodes);
        }

        [HttpGet("{episodeId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(EpisodeDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEpisodeById(Guid episodeId) => _episodes.GetById(episodeId) is null ? BadRequest("Episode does not exist") : new OkObjectResult(DbMapper.EpisodeToDto(_episodes.GetById(episodeId)));
    }
}
