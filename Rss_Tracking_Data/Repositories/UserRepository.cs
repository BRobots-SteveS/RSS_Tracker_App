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
    }
    public class UserRepository : IUserRepository
    {
        private readonly Rss_TrackingDbContext _context;
        public UserRepository(Rss_TrackingDbContext context) { _context = context; }
        public User? GetUserById(Guid id) => _context.Users.Where(x => x.Id == id).FirstOrDefault();
        public List<User> GetAllUsers() => _context.Users.ToList();
        public List<User> GetUsersByName(string name) => _context.Users.Where(x => x.UserName == name).ToList();
        public User AddUser(User User) => _context.Users.Add(User).Entity;
        public User UpdateUser(User User) => _context.Users.Update(User).Entity;
        public void DeleteUser(User User) => _context.Users.Remove(User);
    }
}
