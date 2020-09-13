using System;
using System.Linq;
using System.Security.Cryptography;

namespace Chattle
{
    public class Account : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Username { get; internal set; }
        public bool IsActive { get; internal set; }
        public DateTime CreationTime { get; private set; }

        private string passwordHash;
        private string passwordSalt;

        public Account(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            IsActive = true;
            passwordHash = String.Empty;
            passwordSalt = String.Empty;
            ChangePassword(GenerateRandomPassword());
            CreationTime = DateTime.UtcNow;
        }

        internal void ChangePassword(string password)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 100000, HashAlgorithmName.SHA512);

            passwordHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(16));
            passwordSalt = Convert.ToBase64String(rfc2898DeriveBytes.Salt);
        }

        public bool VerifyPassword(string passwordToVerify)
        {
            var salt = Convert.FromBase64String(passwordSalt);
            var hash = Convert.FromBase64String(passwordHash);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordToVerify, salt, 100000, HashAlgorithmName.SHA512);

            var newHash = rfc2898DeriveBytes.GetBytes(16);
            return newHash.SequenceEqual(hash);
        }

        private string GenerateRandomPassword()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
        }
    }
}
