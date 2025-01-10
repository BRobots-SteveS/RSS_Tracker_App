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
        public static List<FeedDto> FeedToDto(Feed feed, List<Author> authors)
        {
            string creatorId = string.Empty;
            if (feed.Platform == Rss_Tracking_Data.Enums.Platform.Omny)
            { creatorId = $"{feed.ChannelId}/{feed.PlaylistId}"; }
            else if (feed.Platform == Rss_Tracking_Data.Enums.Platform.Youtube)
                creatorId = string.IsNullOrEmpty(feed.PlaylistId) ?
                    string.IsNullOrEmpty(feed.ChannelId) ?
                        feed.CreatorId : $"yt:channel:{feed.ChannelId}"
                    : $"yt:playlist:{feed.PlaylistId}";
            return authors.Select(author => new FeedDto
            {
                Id = feed.Id,
                Title = feed.Title,
                AuthorName = author.Name,
                AuthorEmail = author.Email,
                AuthorUri = author.Uri,
                CreatorId = creatorId,
                Description = feed.Description,
                ThumbnailUri = feed.ImageUrl,
                FeedUri = feed.FeedUrl,
                PublishedTime = feed.PublishDate,
                Platform = feed.Platform.ToString(),
            }).ToList();
        }

        public static EpisodeDto EpisodeToDto(Episode episode) => new()
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
        public static UserFavoriteDto UserFavoriteToDto(UserFavorite userFavorite, Guid? authorId)
        {
            Feed tempFeed = userFavorite.Feed ?? new() { Description = string.Empty, Title = string.Empty, FeedUrl = string.Empty, Id = userFavorite.FeedId };
            Author tempAuthor = userFavorite.Author ?? new() { Name = string.Empty, Id = authorId.GetValueOrDefault(Guid.Empty)};
            return new()
            {
                Id = userFavorite.Id,
                User = UserToDto(new() { Id = userFavorite.UserId }),
                Feed = FeedToDto(tempFeed, [tempAuthor]).First(),
                Author = !authorId.HasValue? null : AuthorToDto(tempAuthor, [tempFeed])
            };
        }
    }
}
