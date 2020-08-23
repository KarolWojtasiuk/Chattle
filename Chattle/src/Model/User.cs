using System;

namespace Chattle
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Username { get; set; }
        public bool IsActive { get; set; }
        public UserType Type { get; private set; }
        public DateTime CreationTime { get; private set; }

        public User(string username, UserType type)
        {
            Id = Guid.NewGuid();
            Username = username;
            IsActive = true;
            Type = type;
            CreationTime = DateTime.UtcNow;
        }
    }

    public enum UserType
    {
        User,
        Bot
    }
}
