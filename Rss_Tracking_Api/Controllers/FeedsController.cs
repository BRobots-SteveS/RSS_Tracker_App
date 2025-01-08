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
            HashSet<FeedDto> output = new();
            var feeds = _feeds.GetAll().Select(x => DbMapper.FeedToDto(x, _authors.GetAuthorsByFeedId(x.Id)));
            foreach (var feedList in feeds) { foreach (var feed in feedList) { output.Add(feed); } }
            return new OkObjectResult(output);
        }

        [HttpGet("{feedId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(FeedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedById(Guid feedId)
        {
            var feed = _feeds.GetById(feedId);
            if (feed == null) return BadRequest("Feed does not exist");
            var authors = _authors.GetAuthorsByFeedId(feedId);
            return new OkObjectResult(DbMapper.FeedToDto(feed, authors).First());
        }

        [HttpGet("author/{authorId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<FeedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedsByAuthorId(Guid authorId)
        {
            HashSet<FeedDto> output = new();
            var temp = _feeds.GetFeedsByAuthorId(authorId).Select(x => DbMapper.FeedToDto(x, [_authors.GetById(authorId)])).ToList();
            foreach(var feedList in temp) { foreach(var feed in feedList) { output.Add(feed); } }
            return new OkObjectResult(output.ToList());
        }

        [HttpGet("user/{userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<FeedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedsByUserId(Guid userId)
        {
            HashSet<FeedDto> output = new();
            var temp = _feeds.GetFeedsByUserId(userId).Select(x => DbMapper.FeedToDto(x, _authors.GetAuthorsByFeedId(x.Id))).ToList();
            foreach (var feedList in temp) { foreach (var feed in feedList) { output.Add(feed); } }
            return new OkObjectResult(output.ToList());
        }

        [HttpGet("filter")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<FeedDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFeedsByFilter([FromQuery]string? title, [FromQuery]string? creatorId, [FromQuery]string? description, [FromQuery]string? authorName, [FromQuery]string? platform)
        {
            HashSet<FeedDto> result = new();
            var feeds = _feeds.GetFeedsByFilter(title, creatorId, description, authorName, platform).Select(x => DbMapper.FeedToDto(x, _authors.GetAuthorsByFeedId(x.Id)));
            foreach (var feedList in feeds) { foreach (var feed in feedList) { result.Add(feed); } }
            var output = result.ToList();
            output.Sort((x, y) => DateTime.Compare(x.PublishedTime, y.PublishedTime));
            return new OkObjectResult(output);
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(FeedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFeed([FromBody] FeedDto dto)
        {
            string uri = string.Empty;
            if (dto.Platform == Platform.Youtube.ToString())
            {
                if (dto.CreatorId.StartsWith("yt:channel:"))
                    uri = $"https://www.youtube.com/feeds/videos.xml?channel_id={dto.CreatorId.Replace("yt:channel:", string.Empty)}";
                else if (dto.CreatorId.StartsWith("yt:playlist:"))
                    uri = $"https://www.youtube.com/feeds/videos.xml?playlist_id={dto.CreatorId.Replace("yt:playlist:", string.Empty)}";
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
            if (syndicate.Links.Count > 0)
                if (syndicate.Links.First().Uri.AbsoluteUri.StartsWith("https://media.rss.com"))
                    syndicate.Links[0].Uri = new Uri($"{syndicate.Links[0].Uri.AbsoluteUri}/feed.xml");
            if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == YoutubeFeed.NAMESPACE))
                feed = FeedMapper.FeedToDbObject(new YoutubeFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == OmnyFeed.NAMESPACE))
                feed = FeedMapper.FeedToDbObject(new OmnyFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == ITunesFeed.NAMESPACE))
                feed = FeedMapper.FeedToDbObject(new ITunesFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == MediaFeed.NAMESPACE))
                feed = FeedMapper.FeedToDbObject(new MediaFeed(syndicate), out authors, out episodes);
            else
                return BadRequest("Given feed to not conform to Atom1, RSS2 standard with mRSS");
            if (!_feeds.AlreadyExists(feed))
                resultFeed = _feeds.Add(feed);
            else
                resultFeed = _feeds.Update(feed)!;
            foreach (var author in authors)
            {
                Author resultAuthor;
                if (!_authors.AlreadyExists(author))
                    resultAuthor = _authors.Add(author);
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
                if (!_episodes.AlreadyExists(episode))
                    resultEpisode = _episodes.Add(episode);
                else
                    resultEpisode = _episodes.GetEpisodeByData(episode) ?? throw new FileNotFoundException($"Failed to find existing Episode: {System.Text.Json.JsonSerializer.Serialize(episode)}");
            }
            _feeds.SaveChanges();
            return new OkObjectResult(DbMapper.FeedToDto(resultFeed, resultAuthors));
        }

        [HttpPut("{feedId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(FeedDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateFeed(Guid feedId)
        {
            var feed = _feeds.GetById(feedId);
            if (feed == null || feedId == Guid.Empty) return NotFound($"No feed for given id.");
            List<Author> authors = new();
            List<Episode> episodes = new();
            Feed resultFeed;
            List<Author> resultAuthors = new();
            var syndicate = FeedHelper.GetLatestFeed(new Uri(feed.FeedUrl));
            if (syndicate == null) return BadRequest("Failed to fetch data from URL with given parameters.");
            if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == YoutubeFeed.NAMESPACE))
                resultFeed = FeedMapper.FeedToDbObject(new YoutubeFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == OmnyFeed.NAMESPACE))
                resultFeed = FeedMapper.FeedToDbObject(new OmnyFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == ITunesFeed.NAMESPACE))
                resultFeed = FeedMapper.FeedToDbObject(new ITunesFeed(syndicate), out authors, out episodes);
            else if (syndicate.ElementExtensions.Any(x => x.OuterNamespace == MediaFeed.NAMESPACE))
                resultFeed = FeedMapper.FeedToDbObject(new MediaFeed(syndicate), out authors, out episodes);
            else
                return BadRequest("Given feed to not conform to Atom1, RSS2 standard with mRSS");
            resultFeed.Id = feed.Id;
            _feeds.Update(resultFeed);

            foreach (var author in authors)
            {
                Author resultAuthor;
                if (!_authors.AlreadyExists(author))
                    resultAuthor = _authors.Add(author);
                else
                {
                    var tempAuthor = _authors.GetAuthorsByUri(author.Uri).First();
                    author.Id = tempAuthor.Id;
                    resultAuthor = _authors.Update(author);
                }
                if (!_authors.DoesFeedAuthorExist(resultFeed.Id, resultAuthor.Id))
                    _authors.AddFeedAuthor(resultFeed.Id, resultAuthor.Id);
                resultAuthors.Add(resultAuthor);
            }
            foreach (var episode in episodes)
            {
                episode.FeedId = resultFeed.Id;
                if (!_episodes.AlreadyExists(episode))
                    _episodes.Add(episode);
                else
                {
                    var tempEpisode = _episodes.GetEpisodeByData(episode); episode.Id = tempEpisode.Id;
                    _episodes.Update(episode);
                }
            }
            _feeds.SaveChanges();
            return new OkObjectResult(DbMapper.FeedToDto(resultFeed, resultAuthors).FirstOrDefault());
        }
    }
}
