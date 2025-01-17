﻿using Rss_Tracking_Data.Helpers;
using System.Text;

namespace Rss_Tracking_Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; private set; }
        public string Salt { get; private set; }

        public User() { Id = Guid.Empty; UserName = string.Empty; Password = string.Empty; Salt = string.Empty; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = CryptoHelper.HashPassword(password, null, out var salt);
            Salt = Convert.ToHexString(salt);
        }
        public void UpdatePassword(string newPassword)
        {
            var realPassword = System.Text.Encoding.UTF8.GetString(Convert.FromHexString(newPassword));
            var newHash = CryptoHelper.HashPassword(realPassword, Salt, out _);
            if (newHash != Password) Password = newHash;
        }
        public bool ValidLogin(string userName, string password)
        { return UserName == userName && Password == CryptoHelper.HashPassword(password, Salt, out _); }
    }
}
