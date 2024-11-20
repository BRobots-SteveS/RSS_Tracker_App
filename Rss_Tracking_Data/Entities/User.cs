using Rss_Tracking_Data.Helpers;
using System.Text;

namespace Rss_Tracking_Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = CryptoHelper.HashPassword(password, null, out var salt);
            Salt = Encoding.UTF8.GetString(salt);
        }
        public void UpdatePassword(string newPassword)
        {
            var newHash = CryptoHelper.HashPassword(newPassword, Salt, out _);
            if (newHash != Password) Password = newHash;
        }
        public bool ValidLogin(string userName, string password)
        { return UserName == userName && Password == CryptoHelper.HashPassword(password, Salt, out _); }
    }
}
