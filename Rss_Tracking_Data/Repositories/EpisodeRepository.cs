using Microsoft.EntityFrameworkCore;
using Rss_Tracking_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Data.Repositories
{
    public interface IEpisodeRepository : IBaseRepository<Episode>
    {
        Episode? GetEpisodeByData(Episode episode);
        List<Episode> GetEpisodesByName(string name);
        List<Episode> GetEpisodesByFeedId(Guid feedId);
        List<Episode> GetEpisodesByUri(string uri);
    }
    public class EpisodeRepository : BaseRepository<Episode>, IEpisodeRepository
    {
        public EpisodeRepository(Rss_TrackingDbContext context) : base(context) { }
        public override Episode? GetById(Guid id) => _context.Episodes.AsNoTracking().SingleOrDefault(x => x.Id == id);
        public override List<Episode> GetAll() => _context.Episodes.AsNoTracking().ToList();
        public override Episode Add(Episode episode)
        {
            episode.Id = Guid.NewGuid();
            return _context.Episodes.Add(episode).Entity;
        }

        public override Episode Update(Episode episode) => _context.Episodes.Update(episode).Entity;
        public override void Delete(Episode episode) => _context.Episodes.Remove(episode);
        public override bool AlreadyExists(Episode episode) => _context.Episodes.AsNoTracking().Any(x => x.EpisodeId == episode.EpisodeId && x.Url == episode.Url && x.Description == episode.Description && x.ThumbnailUrl == episode.ThumbnailUrl && x.Name == episode.Name);
        public Episode? GetEpisodeByData(Episode episode) => _context.Episodes.AsNoTracking().Where(x => x.EpisodeId == episode.EpisodeId && x.Url == episode.Url && x.Description == episode.Description && x.ThumbnailUrl == episode.ThumbnailUrl && x.Name == episode.Name).FirstOrDefault();
        public List<Episode> GetEpisodesByName(string name) => _context.Episodes.AsNoTracking().Where(x => x.Name == name).ToList();
        public List<Episode> GetEpisodesByFeedId(Guid feedId) => _context.Episodes.AsNoTracking().Where(x => x.FeedId == feedId).ToList();
        public List<Episode> GetEpisodesByUri(string uri) => _context.Episodes.AsNoTracking().Where(x => x.Url == uri).ToList();
    }
}
