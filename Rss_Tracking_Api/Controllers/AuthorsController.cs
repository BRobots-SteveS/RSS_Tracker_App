using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Repositories;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _authors;
        private readonly IFeedRepository _feeds;
        private readonly ILogger<AuthorsController> _logger;
        public AuthorsController(ILogger<AuthorsController> logger, IEpisodeRepository episodes, IFeedRepository feeds, IAuthorRepository authors)
        {
            _logger = logger;
            _authors = authors;
            _feeds = feeds;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAuthors()
        {
            return new OkObjectResult(_authors.GetAll().Select(x => DbMapper.AuthorToDto(x, _feeds.GetFeedsByAuthorId(x.Id))).ToList());
        }

        [HttpGet("feed/{feedId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthorsByFeedId(Guid feedId)
        {
            return new OkObjectResult(_authors.GetAuthorsByFeedId(feedId).Select(x => DbMapper.AuthorToDto(x, [_feeds.GetById(feedId)])).ToList());
        }

        [HttpGet("{authorId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthorById(Guid authorId)
        {
            return _authors.GetById(authorId) is null ? BadRequest("Episode does not exist") : new OkObjectResult(DbMapper.AuthorToDto(_authors.GetById(authorId), _feeds.GetFeedsByAuthorId(authorId)));
        }

        [HttpGet("filter")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAuthorByFilter([FromQuery] string? authorName, [FromQuery] string? email)
        {
            return new OkObjectResult(_authors.GetAuthorsByNameAndEmail(authorName, email).Select(x => DbMapper.AuthorToDto(x, _feeds.GetFeedsByAuthorId(x.Id))).ToList());
        }
    }
}
