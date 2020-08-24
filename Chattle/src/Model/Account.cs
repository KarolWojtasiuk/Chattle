using System;

namespace Chattle
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; private set; }
        public DateTime CreationTime { get; private set; }

        public Account(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            IsActive = true;
            PasswordHash = String.Empty;
            PasswordSalt = String.Empty;
            ChangePassword(password);
            CreationTime = DateTime.UtcNow;
        }

        public void ChangePassword(string password)
        {
            throw new NotImplementedException();
            //TODO: Implement password hashing;
        }

        public void VerifyPassword(string passwordToVerify)
        {
            throw new NotImplementedException();
            //TODO: Implement password verification;
        }
    }
}
