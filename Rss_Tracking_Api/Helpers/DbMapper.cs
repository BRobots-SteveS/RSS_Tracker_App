using Rss_Tracking_Api.Models.Dto;
using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Api.Helpers
{
    public static class DbMapper
    {
        public static AuthorDto AuthorToDto(Author author, List<Feed> feeds) => new()
        {
            Id = author.Id,
            FeedIds = feeds.Select(x => x.Id).ToList(),
            Name = author.Name,
            Email = author.Email,
            Uri = author.Uri
        };
        public static FeedDto CreatorToDto(Feed creator, List<Author> authors) => new()
        {
            Id = creator.Id,
            AuthorId = authors.Select(x => x.Id),
            CreatorId = creator.CreatorId,
            Description = creator.Description,
            ThumbnailUri = creator.ImageUrl,
            FeedUri = creator.FeedUrl,
            PublishedTime = creator.PublishDate,
            Platform = creator.Platform.ToString(),
        };
        public static EpisodeDto EpisodeToDto(Episode episode, Author firstAuthor) => new()
        {
            Id = episode.Id,
            EpisodeId = episode.EpisodeId,
            EpisodeName = episode.Name,
            CreatedOn = episode.Published,
            PreviewUrl = episode.Url,
            Description = episode.Description,
            FeedId = episode.FeedId,
        };
        public static UserDto UserToDto(User user) => new()
        {
            Id = user.Id,
            Username = user.UserName,
            Password = string.Empty,
        };
        public static UserFavoriteDto UserFavoriteToDto(UserFavorite userFavorite) => new()
        {
            Id = userFavorite.Id,
            User = UserToDto(userFavorite.User),
            Creator = CreatorToDto(userFavorite.Feed, [])
        };
    }
}
