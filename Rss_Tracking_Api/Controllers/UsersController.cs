using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Helpers;
using Rss_Tracking_Data.Repositories;
using System.Text;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _users;
        private readonly IUserFavoriteRepository _favorites;
        private readonly IFeedRepository _feeds;
        private readonly IAuthorRepository _authors;
        private ILogger<UsersController> _logger;
        public UsersController(
            ILogger<UsersController> logger,
            IUserRepository users, IUserFavoriteRepository favorites,
            IFeedRepository feeds, IAuthorRepository authors)
        {
            _logger = logger;
            _users = users;
            _favorites = favorites;
            _feeds = feeds;
            _authors = authors;
        }

        [HttpGet("login")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser()
        {
            if (Request.Headers.Authorization.Count <= 0) return BadRequest("Requires basic auth header");
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            var values = authHeader.Replace("Basic ", "").Split(':');
            var username = values[0];
            var realPassword = System.Text.Encoding.UTF8.GetString(Convert.FromHexString(values[1]));
            var users = _users.GetUsersByName(username);
            if (users is null || !users.Any()) return NotFound("No user exists for the given username");
            foreach (var user in users)
            {
                if (CryptoHelper.ValidatePassword(realPassword, user.Salt, user.Password)) return new OkObjectResult(DbMapper.UserToDto(user));
                continue;
            }
            return BadRequest("Wrong password, try again");
        }

        [HttpGet("favorite/{userId}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(List<UserFavoriteDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyFavorites(Guid userId)
        {
            return new OkObjectResult(_favorites.GetUserFavoritesByUserId(userId).Select(x => DbMapper.UserFavoriteToDto(x, x.Author ?? new() { Name = string.Empty })).ToList());
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser()
        {
            if (Request.Headers.Authorization.Count <= 0) return BadRequest("Requires basic auth header");
            var authHeader = Request.Headers.Authorization.FirstOrDefault();
            var values = authHeader.Replace("Basic ", "").Split(':');
            var username = values[0];
            var password = System.Text.Encoding.UTF8.GetString(Convert.FromHexString(values[1]));
            if (_users.IsUsernameTaken(username)) return BadRequest("This username is already taken");
            User user = new(username, password) { UserName = username };
            return new OkObjectResult(DbMapper.UserToDto(_users.AddUser(user)));
        }
        [HttpPost("favorite")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(UserFavoriteDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateFavorite([FromQuery] Guid userId, [FromQuery] Guid feedId, [FromQuery] Guid? authorId)
        {
            if (_favorites.GetUserFavoritesByUserId(userId).Any(x => x.FeedId == feedId && x.AuthorId == authorId))
                return BadRequest("Already exists");
            var author = _authors.GetAuthorById(authorId ?? Guid.Empty);
            var feed = _feeds.GetFeedById(feedId);
            if (author == null || feed == null) return BadRequest("Feed or Author do not exist");
            return new OkObjectResult(DbMapper.UserFavoriteToDto(_favorites.AddUserFavorite(new() { UserId = userId, FeedId = feedId, AuthorId = authorId }), author));
        }
        [HttpDelete("favorite")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteFavorite([FromQuery] Guid favoriteId, [FromQuery] Guid userId)
        {
            var favorite = _favorites.GetUserFavoriteById(favoriteId);
            if (favorite == null) return BadRequest("Favorite doesn't exist");
            if (_users.GetUserById(userId) == null) return BadRequest("User doesn't exist");
            var favorites = _favorites.GetUserFavoritesByUserId(userId);
            if (favorites == null || !favorites.Any()) return BadRequest("You do not have any favorites yet");
            if (!favorites.Any(x => x.Id == favoriteId)) return BadRequest("You do not own this favorite");
            _favorites.DeleteUserFavorite(favorite);
            return NoContent();
        }
        [HttpDelete]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid userId)
        {
            var user = _users.GetUserById(userId);
            if (user == null) return BadRequest("User doesn't exist");
            _users.DeleteUser(user);
            return NoContent();
        }
    }
}
