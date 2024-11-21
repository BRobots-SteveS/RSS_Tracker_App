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
        Episode? GetEpisodeById(Guid id);
        Episode? GetEpisodeByData(Episode episode);
        List<Episode> GetAllEpisodes();
        List<Episode> GetEpisodesByName(string name);
        List<Episode> GetEpisodesByFeedId(Guid feedId);
        List<Episode> GetEpisodesByUri(string uri);
        Episode AddEpisode(Episode Episode);
        Episode UpdateEpisode(Episode Episode);
        void DeleteEpisode(Episode Episode);
        bool DoesEpisodeExist(Episode episode);
    }
    public class EpisodeRepository : IEpisodeRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public EpisodeRepository(Rss_TrackingDbContext context) { _context = context; }
        public Episode? GetEpisodeById(Guid id) => _context.Episodes.Where(x => x.Id == id).FirstOrDefault();
        public Episode? GetEpisodeByData(Episode episode) => _context.Episodes.Where(x => x.EpisodeId == episode.EpisodeId && x.Url == episode.Url && x.Description == episode.Description && x.ThumbnailUrl == episode.ThumbnailUrl && x.Name == episode.Name).FirstOrDefault();
        public List<Episode> GetAllEpisodes() => _context.Episodes.ToList();
        public List<Episode> GetEpisodesByName(string name) => _context.Episodes.Where(x => x.Name == name).ToList();
        public List<Episode> GetEpisodesByFeedId(Guid feedId) => _context.Episodes.Where(x => x.FeedId == feedId).ToList();
        public List<Episode> GetEpisodesByUri(string uri) => _context.Episodes.Where(x => x.Url == uri).ToList();
        public Episode AddEpisode(Episode episode)
        {
            episode.Id = Guid.NewGuid();
            return _context.Episodes.Add(episode).Entity;
        }

        public Episode UpdateEpisode(Episode episode) => _context.Episodes.Update(episode).Entity;
        public void DeleteEpisode(Episode episode) => _context.Episodes.Remove(episode);
        public bool DoesEpisodeExist(Episode episode) => _context.Episodes.Any(x => x.EpisodeId == episode.EpisodeId && x.Url == episode.Url && x.Description == episode.Description && x.ThumbnailUrl == episode.ThumbnailUrl && x.Name == episode.Name);
    }
}
