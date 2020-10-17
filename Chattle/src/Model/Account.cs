using System;
using System.Linq;
using System.Security.Cryptography;
using MongoDB.Bson.Serialization.Attributes;

namespace Chattle
{
    public class Account : IIdentifiable, IEquatable<Account>
    {
        public Guid Id { get; internal set; }
        public string Username { get; internal set; }
        public bool IsActive { get; internal set; }
        public AccountGlobalPermission GlobalPermissions { get; internal set; }
        public DateTime CreationTime { get; private set; }

        [BsonElement("PasswordHash")] private string _passwordHash;
        [BsonElement("PasswordSalt")] private string _passwordSalt;

        public Account(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            IsActive = true;
            GlobalPermissions = AccountGlobalPermission.None;
            _passwordHash = String.Empty;
            _passwordSalt = String.Empty;
            ChangePassword(GenerateRandomPassword());
            CreationTime = DateTime.UtcNow;
        }

        internal void ChangePassword(string password)
        {
            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 100000, HashAlgorithmName.SHA512);

            _passwordHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(16));
            _passwordSalt = Convert.ToBase64String(rfc2898DeriveBytes.Salt);
        }

        public bool VerifyPassword(string passwordToVerify)
        {
            var salt = Convert.FromBase64String(_passwordSalt);
            var hash = Convert.FromBase64String(_passwordHash);

            using var rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordToVerify, salt, 100000, HashAlgorithmName.SHA512);

            var newHash = rfc2898DeriveBytes.GetBytes(16);
            return newHash.SequenceEqual(hash);
        }

        private string GenerateRandomPassword()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
        }

        public bool Equals(Account other)
        {
            return other.Id == Id;
        }
    }
}
