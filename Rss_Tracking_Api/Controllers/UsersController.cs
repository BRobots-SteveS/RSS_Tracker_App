using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Rss_Tracking_Api.Helpers;
using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Enums;
using Rss_Tracking_Data.Helpers;
using Rss_Tracking_Data.Repositories;
using Rss_Tracking_Lib.Models;
using System.Text;

namespace Rss_Tracking_Api.Controllers
{
    [ApiController]
    [ApiVersion(1.0f)]
    [Route("api/v[version:apiVersion]/[controller]")]

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
        public async Task<ActionResult<UserDto>> GetUser([FromQuery] string username, [FromQuery] string password)
        {
            var realPassword = Encoding.UTF8.GetString(Convert.FromHexString(password));
            var users = _users.GetUsersByName(username);
            if (users is null || !users.Any()) return NotFound("No user exists for the given username");
            foreach (var user in users)
            {
                if (CryptoHelper.ValidatePassword(password, user.Salt, user.Password)) return Ok(DbMapper.UserToDto(user));
                continue;
            }
            return BadRequest("Wrong password, try again");
        }

        [HttpGet("favorite/{userId}")]
        public async Task<ActionResult<List<UserFavoriteDto>>> GetMyFavorites(Guid userId)
        {
            return _favorites.GetUserFavoritesByUserId(userId).Select(x => DbMapper.UserFavoriteToDto(x, x.Author ?? new() { Name = string.Empty })).ToList();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromQuery] string username, [FromQuery] string password)
        {
            if (_users.IsUsernameTaken(username)) return BadRequest("This username is already taken");
            User user = new(username, password) { UserName = username };
            return Ok(DbMapper.UserToDto(_users.AddUser(user)));
        }
        [HttpPost("favorite")]
        public async Task<ActionResult<UserFavoriteDto>> CreateFavorite([FromQuery]Guid userId, [FromQuery]Guid feedId, [FromQuery]Guid? authorId)
        {
            if (_favorites.GetUserFavoritesByUserId(userId).Any(x => x.FeedId == feedId && x.AuthorId == authorId))
                return BadRequest("Already exists");
            var author = _authors.GetAuthorById(authorId?? Guid.Empty);
            var feed = _feeds.GetFeedById(feedId);
            if (author == null || feed == null) return BadRequest("Feed or Author do not exist");
            return Ok(DbMapper.UserFavoriteToDto(_favorites.AddUserFavorite(new() { UserId = userId, FeedId = feedId, AuthorId = authorId }), author));
        }
        [HttpDelete("favorite")]
        public async Task<IActionResult> DeleteFavorite([FromQuery]Guid favoriteId, [FromQuery]Guid userId)
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
        public async Task<IActionResult> DeleteUser([FromQuery]Guid userId)
        {
            var user = _users.GetUserById(userId);
            if (user == null) return BadRequest("User doesn't exist");
            _users.DeleteUser(user);
            return NoContent();
        }
    }
}
