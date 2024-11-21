using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Enums;

namespace Rss_Tracking_Data.Repositories
{
    public interface IFeedRepository
    {
        Feed? GetFeedById(Guid id);
        List<Feed> GetAllFeeds();
        List<Feed> GetFeedsByPlatform(Platform platform);
        List<Feed> GetFeedsByCreatorId(string creatorId);
        List<Feed> GetFeedsByAuthorId(Guid authorId);
        List<Feed> GetFeedsByUri(string uri);
        Feed AddFeed(Feed Feed);
        Feed UpdateFeed(Feed Feed);
        void DeleteFeed(Feed Feed);
        bool DoesFeedAlreadyExist(Feed feed);

    }
    public class FeedRepository : IFeedRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public FeedRepository(Rss_TrackingDbContext context) { _context = context; }
        public Feed? GetFeedById(Guid id) => _context.Feeds.Where(x => x.Id == id).FirstOrDefault();
        public List<Feed> GetAllFeeds() => _context.Feeds.ToList();
        public Feed AddFeed(Feed Feed)
        {
            Feed.Id = Guid.NewGuid();
            return _context.Feeds.Add(Feed).Entity;
        }

        public Feed UpdateFeed(Feed Feed) => _context.Feeds.Update(Feed).Entity;
        public void DeleteFeed(Feed Feed) => _context.Feeds.Remove(Feed);
        public List<Feed> GetFeedsByPlatform(Platform platform) => _context.Feeds.Where(x => x.Platform == platform).ToList();
        public List<Feed> GetFeedsByCreatorId(string creatorId) => _context.Feeds.Where(x => x.CreatorId == creatorId).ToList();
        public List<Feed> GetFeedsByAuthorId(Guid authorId) => _context.FeedsAuthors.Where(x => x.AuthorId == authorId).Select(x => x.Feed).ToList();
        public List<Feed> GetFeedsByUri(string uri) => _context.Feeds.Where(x => x.FeedUrl == uri).ToList();
        public bool DoesFeedAlreadyExist(Feed feed) => _context.Feeds.Any(x => x.ChannelId == feed.ChannelId && x.PlaylistId == feed.PlaylistId && x.CreatorId == feed.CreatorId && x.FeedUrl == feed.FeedUrl);

    }
}
