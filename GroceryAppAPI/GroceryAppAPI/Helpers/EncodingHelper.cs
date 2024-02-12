using System.Security.Cryptography;
using System.Text;
namespace GroceryAppAPI.Helpers
{
    // A helper class for encoding strings.
    public static class EncodingHelper
    {
        // Generate the 256 bit hash of a password using SHA-256 bit.
        public static string HashPassword(string password)
        {
            var encodedPassword = Encoding.UTF8.GetBytes(password);
            var passwordHash = SHA256.HashData(encodedPassword);
            return Convert.ToBase64String(passwordHash);
        }
    }
}
