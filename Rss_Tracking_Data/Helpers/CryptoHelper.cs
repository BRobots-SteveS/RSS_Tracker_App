using System.Security.Cryptography;
using System.Text;

namespace Rss_Tracking_Data.Helpers
{
    public static class CryptoHelper
    {
        private const int KeySize = 64;
        private const int Itterations = 50000;
        private static readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA512;
        public static string HashPassword(string clearPassword, string? existingSalt, out byte[] salt)
        {
            salt = string.IsNullOrEmpty(existingSalt) ? RandomNumberGenerator.GetBytes(KeySize) : Convert.FromHexString(existingSalt);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(clearPassword),
                salt,
                Itterations,
                AlgorithmName,
                KeySize);
            return Convert.ToHexString(hash);
        }
        public static bool ValidatePassword(string password, string salt, string hashedPassword)
        {
            return hashedPassword == HashPassword(password, salt, out _);
        }
        public static string GenerateSalt()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(KeySize));
        }
    }
}
