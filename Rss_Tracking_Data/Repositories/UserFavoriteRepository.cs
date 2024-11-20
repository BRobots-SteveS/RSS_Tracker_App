using Rss_Tracking_Data.Entities;

namespace Rss_Tracking_Data.Repositories
{
    public interface IUserFavoriteRepository
    {
        UserFavorite GetUserFavoriteById(Guid id);
        List<UserFavorite> GetAllUserFavorites();
        List<UserFavorite> GetUserFavoritesByUserId(Guid userId);
        UserFavorite AddUserFavorite(UserFavorite UserFavorite);
        UserFavorite UpdateUserFavorite(UserFavorite UserFavorite);
        void DeleteUserFavorite(UserFavorite UserFavorite);
    }
    public class UserFavoriteRepository : IUserFavoriteRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public UserFavoriteRepository(Rss_TrackingDbContext context) { _context = context; }
        public UserFavorite GetUserFavoriteById(Guid id) => _context.UserFavorites.Where(x => x.Id == id).FirstOrDefault();
        public List<UserFavorite> GetAllUserFavorites() => _context.UserFavorites.ToList();
        public List<UserFavorite> GetUserFavoritesByUserId(Guid userId) => _context.UserFavorites.Where(x => x.UserId == userId).ToList();
        public UserFavorite AddUserFavorite(UserFavorite UserFavorite) => _context.UserFavorites.Add(UserFavorite).Entity;
        public UserFavorite UpdateUserFavorite(UserFavorite UserFavorite) => _context.UserFavorites.Update(UserFavorite).Entity;
        public void DeleteUserFavorite(UserFavorite UserFavorite) => _context.UserFavorites.Remove(UserFavorite);
    }
}
