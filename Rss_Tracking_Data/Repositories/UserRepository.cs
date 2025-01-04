using Rss_Tracking_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Data.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        List<User> GetUsersByName(string name);
        bool IsUsernameTaken(string username);
    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Rss_TrackingDbContext context) : base(context) { }
        public override User? GetById(Guid id) => _context.Users.SingleOrDefault(x => x.Id == id);
        public override List<User> GetAll() => _context.Users.ToList();
        public override User Add(User user)
        {
            user.Id = Guid.NewGuid();
            return _context.Users.Add(user).Entity;
        }

        public override User Update(User user) => _context.Users.Update(user).Entity;
        public override void Delete(User user) => _context.Users.Remove(user);
        public override bool AlreadyExists(User user) => IsUsernameTaken(user.UserName);
        public bool IsUsernameTaken(string username) => _context.Users.Any(x => x.UserName == username);
        public List<User> GetUsersByName(string name) => _context.Users.Where(x => x.UserName == name).ToList();
    }
}
