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
    }
    public class FeedRepository : BaseRepository<Feed>, IFeedRepository
    {
        public FeedRepository(Rss_TrackingDbContext context) : base(context) { }
        public override Feed? GetById(Guid id) => _context.Feeds.Where(x => x.Id == id).FirstOrDefault();
        public override List<Feed> GetAll() => _context.Feeds.ToList();
        public override Feed Add(Feed Feed)
        {
            Feed.Id = Guid.NewGuid();
            return _context.Feeds.Add(Feed).Entity;
        }

        public override Feed Update(Feed Feed) => _context.Feeds.Update(Feed).Entity;
        public override void Delete(Feed Feed) => _context.Feeds.Remove(Feed);
        public override bool AlreadyExists(Feed feed) => _context.Feeds.Any(x => x.ChannelId == feed.ChannelId && x.PlaylistId == feed.PlaylistId && x.CreatorId == feed.CreatorId && x.FeedUrl == feed.FeedUrl);
        public List<Feed> GetFeedsByPlatform(Platform platform) => _context.Feeds.Where(x => x.Platform == platform).ToList();
        public List<Feed> GetFeedsByCreatorId(string creatorId) => _context.Feeds.Where(x => x.CreatorId == creatorId).ToList();
        public List<Feed> GetFeedsByAuthorId(Guid authorId) => _context.FeedsAuthors.Where(x => x.AuthorId == authorId).Select(x => x.Feed).ToList();
        public List<Feed> GetFeedsByUserId(Guid userId) => _context.UserFavorites.Where(x => x.UserId == userId).Select(x => x.Feed).ToList();
        public List<Feed> GetFeedsByUri(string uri) => _context.Feeds.Where(x => x.FeedUrl == uri).ToList();

    }
}
