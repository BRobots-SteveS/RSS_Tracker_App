using Microsoft.EntityFrameworkCore;
using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Enums;

namespace Rss_Tracking_Data.Repositories
{
    public interface IFeedRepository : IBaseRepository<Feed>
    {
        List<Feed> GetFeedsByPlatform(Platform platform);
        List<Feed> GetFeedsByCreatorId(string creatorId);
        List<Feed> GetFeedsByAuthorId(Guid authorId);
        List<Feed> GetFeedsByUserId(Guid userId);
        List<Feed> GetFeedsByUri(string uri);
        List<Feed> GetFeedsByTitle(string title);
        List<Feed> GetFeedsByFilter(string? title, string? creatorId, string? description, string? authorName, string? platform);
        List<Feed> GetFeedsByIds(List<Guid> ids);
    }
    public class FeedRepository : BaseRepository<Feed>, IFeedRepository
    {
        public FeedRepository(Rss_TrackingDbContext context) : base(context) { }
        public override Feed? GetById(Guid id) => _context.Feeds.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public override List<Feed> GetAll() => _context.Feeds.AsNoTracking().ToList();
        public override Feed Add(Feed Feed)
        {
            Feed.Id = Guid.NewGuid();
            return _context.Feeds.Add(Feed).Entity;
        }

        public override Feed Update(Feed Feed) => _context.Feeds.Update(Feed).Entity;
        public override void Delete(Feed Feed) => _context.Feeds.Remove(Feed);
        public override bool AlreadyExists(Feed feed) => _context.Feeds.AsNoTracking().Any(x => x.ChannelId == feed.ChannelId && x.PlaylistId == feed.PlaylistId && x.CreatorId == feed.CreatorId && x.FeedUrl == feed.FeedUrl);
        public List<Feed> GetFeedsByPlatform(Platform platform) => _context.Feeds.AsNoTracking().Where(x => x.Platform == platform).ToList();
        public List<Feed> GetFeedsByCreatorId(string creatorId) => _context.Feeds.AsNoTracking().Where(x => x.CreatorId == creatorId).ToList();
        public List<Feed> GetFeedsByAuthorId(Guid authorId) => _context.FeedsAuthors.AsNoTracking().Where(x => x.AuthorId == authorId).Select(x => x.Feed).ToList();
        public List<Feed> GetFeedsByUserId(Guid userId) => _context.UserFavorites.AsNoTracking().Where(x => x.UserId == userId).Select(x => x.Feed).ToList();
        public List<Feed> GetFeedsByUri(string uri) => _context.Feeds.AsNoTracking().Where(x => x.FeedUrl == uri).ToList();
        public List<Feed> GetFeedsByTitle(string title) => _context.Feeds.AsNoTracking().Where(x => x.Title.Contains(title)).ToList();
        public List<Feed> GetFeedsByFilter(string? title, string? creatorId, string? description, string? authorName, string? platform)
        {
            if (!Enum.TryParse<Platform>(platform, out Platform parsedPlatform)) { parsedPlatform = Platform.Basic; }
            HashSet<Feed> result = new();
            if(!string.IsNullOrEmpty(authorName))
            {
                var feeds = _context.FeedsAuthors.AsNoTracking().Where(x => x.Author.Name == authorName).Select(x => x.Feed).ToList();
                foreach(var feed in feeds) result.Add(feed);
            }
            var filteredFeeds = _context.Feeds.AsNoTracking().Where(x =>
                (string.IsNullOrEmpty(title) || x.Title.Contains(title)) &&
                (string.IsNullOrEmpty(creatorId) || (string.IsNullOrEmpty(x.CreatorId) || x.CreatorId.Contains(creatorId))) &&
                (string.IsNullOrEmpty(description) || x.Description.Contains(description)) &&
                (parsedPlatform == Platform.Basic || x.Platform == parsedPlatform)
            );
            foreach(var feed in filteredFeeds) result.Add(feed);
            return result.ToList();
        }
        public List<Feed> GetFeedsByIds(List<Guid> ids) => _context.Feeds.AsNoTracking().Where(x =>  ids.Contains(x.Id)).ToList();
    }
}
