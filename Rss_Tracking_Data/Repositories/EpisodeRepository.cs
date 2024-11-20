using Rss_Tracking_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Data.Repositories
{
    public interface IEpisodeRepository
    {
        Episode GetEpisodeById(Guid id);
        List<Episode> GetAllEpisodes();
        List<Episode> GetEpisodesByName(string name);
        List<Episode> GetEpisodesByCreator(Feed creator);
        List<Episode> GetEpisodesByUri(string uri);
        Episode AddEpisode(Episode Episode);
        Episode UpdateEpisode(Episode Episode);
        void DeleteEpisode(Episode Episode);
    }
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public EpisodeRepository(Rss_TrackingDbContext context) { _context = context; }
        public Episode GetEpisodeById(Guid id) => _context.Episodes.Where(x => x.Id == id).FirstOrDefault();
        public List<Episode> GetAllEpisodes() => _context.Episodes.ToList();
        public List<Episode> GetEpisodesByName(string name) => _context.Episodes.Where(x => x.Name == name).ToList();
        public List<Episode> GetEpisodesByCreator(Feed feed) => _context.Episodes.Where(x => x.FeedId == feed.Id).ToList();
        public List<Episode> GetEpisodesByUri(string uri) => _context.Episodes.Where(x => x.Url == uri).ToList();
        public Episode AddEpisode(Episode Episode) => _context.Episodes.Add(Episode).Entity;
        public Episode UpdateEpisode(Episode Episode) => _context.Episodes.Update(Episode).Entity;
        public void DeleteEpisode(Episode Episode) => _context.Episodes.Remove(Episode);
    }
}
