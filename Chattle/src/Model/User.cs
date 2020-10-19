using System;

namespace Chattle
{
    public class User : IIdentifiable, IEquatable<User>
    {
        public Guid Id { get; private set; }
        public string Nickname { get; internal set; }
        public Uri Image { get; internal set; }
        public bool IsActive { get; internal set; }
        public UserGlobalPermission GlobalPermission { get; internal set; }
        public Guid AccountId { get; private set; }
        public UserType Type { get; private set; }
        public DateTime CreationTime { get; private set; }

        public User(string nickname, Guid accountId, UserType type)
        {
            Id = Guid.NewGuid();
            Nickname = nickname;
            Image = GetDefaultUserImage();
            IsActive = true;
            GlobalPermission = UserGlobalPermission.None;
            AccountId = accountId;
            Type = type;
            CreationTime = DateTime.UtcNow;
        }

        public User(string nickname, Guid accountId, UserType type, Uri image)
        {
            Id = Guid.NewGuid();
            Nickname = nickname;
            Image = image;
            IsActive = true;
            GlobalPermission = UserGlobalPermission.None;
            AccountId = accountId;
            Type = type;
            CreationTime = DateTime.UtcNow;
        }

        public Uri GetDefaultUserImage() => DefaultImage.GetUserImage(Id);

        public bool Equals(User other)
        {
            return other.Id == Id;
        }
    }

    public enum UserType
    {
        User,
        Bot
    }
}
