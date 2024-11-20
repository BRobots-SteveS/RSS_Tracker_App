using Rss_Tracking_Data.Entities;
using Rss_Tracking_Data.Enums;

namespace Rss_Tracking_Data.Repositories
{
    public interface ICreatorRepository
    {
        Creator GetCreatorById(Guid id);
        List<Creator> GetAllCreators();
        List<Creator> GetCreatorsByPlatform(Platform platform);
        List<Creator> GetCreatorsByCreatorId(string creatorId);
        List<Creator> GetCreatorsByUri(string uri);
        Creator AddCreator(Creator Creator);
        Creator UpdateCreator(Creator Creator);
        void DeleteCreator(Creator Creator);
    }
    public class CreatorRepository : ICreatorRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public CreatorRepository(Rss_TrackingDbContext context) { _context = context; }
        public Creator GetCreatorById(Guid id) => _context.Creators.Where(x => x.Id == id).FirstOrDefault();
        public List<Creator> GetAllCreators() => _context.Creators.ToList();
        public List<Creator> GetCreatorsByPlatform(Platform platform) => _context.Creators.Where(x => x.Platform == platform).ToList();
        public List<Creator> GetCreatorsByCreatorId(string creatorId) => _context.Creators.Where(x => x.CreatorId == creatorId).ToList();
        public List<Creator> GetCreatorsByUri(string uri) => _context.Creators.Where(x => x.FeedUrl == uri).ToList();
        public Creator AddCreator(Creator Creator) => _context.Creators.Add(Creator).Entity;
        public Creator UpdateCreator(Creator Creator) => _context.Creators.Update(Creator).Entity;
        public void DeleteCreator(Creator Creator) => _context.Creators.Remove(Creator);
    }
}
