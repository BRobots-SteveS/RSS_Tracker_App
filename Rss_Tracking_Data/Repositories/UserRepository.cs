using Rss_Tracking_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rss_Tracking_Data.Repositories
{
    public interface IUserRepository
    {
        User? GetUserById(Guid id);
        List<User> GetAllUsers();
        List<User> GetUsersByName(string name);
        User AddUser(User User);
        User UpdateUser(User User);
        void DeleteUser(User User);
        bool IsUsernameTaken(string username);
    }
    public class UserRepository : IUserRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public UserRepository(Rss_TrackingDbContext context) { _context = context; }
        public User? GetUserById(Guid id) => _context.Users.Where(x => x.Id == id).FirstOrDefault();
        public List<User> GetAllUsers() => _context.Users.ToList();
        public List<User> GetUsersByName(string name) => _context.Users.Where(x => x.UserName == name).ToList();
        public User AddUser(User user)
        {
            user.Id = Guid.NewGuid();
            return _context.Users.Add(user).Entity;
        }

        public User UpdateUser(User user) => _context.Users.Update(user).Entity;
        public void DeleteUser(User user) => _context.Users.Remove(user);
        public bool IsUsernameTaken(string username) => _context.Users.Any(x => x.UserName == username);
    }
}
