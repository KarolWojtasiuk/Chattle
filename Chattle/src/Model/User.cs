using System;

namespace Chattle
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Nickname { get; set; }
        public Uri Image { get; set; }
        public bool IsActive { get; set; }
        public UserType Type { get; private set; }
        public DateTime CreationTime { get; private set; }

        public User(string nickname, UserType type)
        {
            Id = Guid.NewGuid();
            Nickname = nickname;
            Image = GetDefaultUserImage();
            IsActive = true;
            Type = type;
            CreationTime = DateTime.UtcNow;
        }

        public User(string nickname, UserType type, Uri image)
        {
            Id = Guid.NewGuid();
            Nickname = nickname;
            Image = image;
            IsActive = true;
            Type = type;
            CreationTime = DateTime.UtcNow;
        }

        private Uri GetDefaultUserImage()
        {
            throw new NotImplementedException();
            //TODO: Implement method;
        }
    }

    public enum UserType
    {
        User,
        Bot
    }
}
