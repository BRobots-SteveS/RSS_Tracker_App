using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Data.Repositories
{
    public interface IUserFavoriteRepository : IBaseRepository<UserFavorite>
    {
        List<UserFavorite> GetUserFavoritesByUserId(Guid userId);
    }
    public class UserFavoriteRepository : BaseRepository<UserFavorite>, IUserFavoriteRepository
    {
        public UserFavoriteRepository(Rss_TrackingDbContext context) : base(context) { }
        public override UserFavorite? GetById(Guid id) => _context.UserFavorites.SingleOrDefault(x => x.Id == id);
        public override List<UserFavorite> GetAll() => _context.UserFavorites.ToList();
        public override UserFavorite Add(UserFavorite userFavorite)
        {
            userFavorite.Id = Guid.NewGuid();
            return _context.UserFavorites.Add(userFavorite).Entity;
        }

        public override UserFavorite Update(UserFavorite userFavorite) => _context.UserFavorites.Update(userFavorite).Entity;
        public override void Delete(UserFavorite userFavorite) => _context.UserFavorites.Remove(userFavorite);
        public List<UserFavorite> GetUserFavoritesByUserId(Guid userId) => _context.UserFavorites.Where(x => x.UserId == userId).ToList();
    }
}
