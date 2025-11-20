using System.Text;
using System.Security.Cryptography;
using LW4_API.Server.Interface;
namespace LW4_API.Server.Realizetion
{
    public class PasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool Verify(string password, string hash)
        {
            var computed = HashPassword(password);
            return computed == hash;
        }
    }
}
