using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Enums;
using Rss_Tracking_Data.Repositories;
using Rss_Tracking_Lib.Models;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FeedsController : ControllerBase
    {
        private readonly IFeedRepository _feeds;
        private readonly IAuthorRepository _authors;
        private readonly IEpisodeRepository _episodes;
        private readonly ILogger<EpisodesController> _logger;
        public FeedsController(ILogger<EpisodesController> logger, IFeedRepository feeds, IAuthorRepository authors, IEpisodeRepository episodes)
        {
            _logger = logger;
            _feeds = feeds;
            _authors = authors;
            _episodes = episodes;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<FeedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFeeds()
        {
            return new OkObjectResult(_feeds.GetAllFeeds().Select(x => DbMapper.FeedToDto(x, _authors.GetAuthorsByFeedId(x.Id))).ToList());
        }

        [HttpGet("{feedId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(FeedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedById(Guid feedId)
        {
            var feed = _feeds.GetFeedById(feedId);
            if (feed == null) return BadRequest("Feed does not exist");
            var authors = _authors.GetAuthorsByFeedId(feedId);
            return new OkObjectResult(DbMapper.FeedToDto(feed, authors));
        }

        [HttpGet("author/{authorId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<FeedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedsByAuthorId(Guid authorId)
        {
            return new OkObjectResult(_feeds.GetFeedsByAuthorId(authorId).Select(x => DbMapper.FeedToDto(x, [_authors.GetAuthorById(authorId)])).ToList());
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(FeedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFeed(FeedDto dto)
        {
            string uri = string.Empty;
            if (dto.Platform == Platform.Youtube.ToString())
            {
                if (dto.CreatorId.StartsWith("yt:channel:"))
                    uri = $"https://wwww.youtube.com/feeds/videos.xml?channel_id={dto.CreatorId.Replace("yt:channel:", string.Empty)}";
                else if (dto.CreatorId.StartsWith("yt:playlist:"))
                    uri = $"https://wwww.youtube.com/feeds/videos.xml?playlist_id={dto.CreatorId.Replace("yt:playlist:", string.Empty)}";
            }
            else if (dto.Platform == Platform.Omny.ToString())
            {
                uri = $"https://www.omnycontent.com/d/playlist/{dto.CreatorId}/podcast.rss";
            }
            else if (!string.IsNullOrEmpty(dto.FeedUri))
                uri = dto.FeedUri;
            else
                return BadRequest("Failed to form a proper Feed URL from given parameters.");
            Feed feed;
            List<Author> authors = new();
            List<Episode> episodes = new();
            Feed resultFeed;
            List<Author> resultAuthors = new();
            var syndicate = FeedHelper.GetLatestFeed(new Uri(uri));
            if (syndicate == null) return BadRequest("Failed to fetch data from URL with given parameters.");
            if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == "yt"))
                feed = FeedMapper.FeedToDbObject((YoutubeFeed)syndicate, out authors, out episodes);
            if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == "omny"))
                feed = FeedMapper.FeedToDbObject((OmnyFeed)syndicate, out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == "itunes"))
                feed = FeedMapper.FeedToDbObject((ITunesFeed)syndicate, out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == "media"))
                feed = FeedMapper.FeedToDbObject((ITunesFeed)syndicate, out authors, out episodes);
            else
                return BadRequest("Given feed to not conform to Atom1, RSS2 standard with mRSS");
            if (!_feeds.DoesFeedAlreadyExist(feed))
                resultFeed = _feeds.AddFeed(feed);
            else
                resultFeed = _feeds.GetFeedsByUri(uri).First();
            foreach (var author in authors)
            {
                Author resultAuthor;
                if (!_authors.DoesAuthorExist(author))
                    resultAuthor = _authors.AddAuthor(author);
                else
                    resultAuthor = _authors.GetAuthorsByName(author.Name).First();
                if (!_authors.DoesFeedAuthorExist(resultFeed.Id, resultAuthor.Id))
                    _authors.AddFeedAuthor(resultFeed.Id, resultAuthor.Id);
                resultAuthors.Add(resultAuthor);
            }
            foreach (var episode in episodes)
            {
                episode.FeedId = resultFeed.Id;
                Episode resultEpisode;
                if (!_episodes.DoesEpisodeExist(episode))
                    resultEpisode = _episodes.AddEpisode(episode);
                else
                    resultEpisode = _episodes.GetEpisodeByData(episode) ?? throw new FileNotFoundException($"Failed to find existing Episode: {System.Text.Json.JsonSerializer.Serialize(episode)}");
            }
            return new OkObjectResult(DbMapper.FeedToDto(resultFeed, resultAuthors));
        }
    }
}
