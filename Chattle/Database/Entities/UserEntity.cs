using System;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record UserEntity : IEntity
    {
        public Guid Id { get; private init; }
        public string Nickname { get; init; }
        public string Status { get; init; }
        public bool IsActive { get; init; }
        public Uri? ImageUri { get; init; }
        public Guid AccountId { get; private init; }
        public UserType UserType { get; private init; }
        public UserGlobalPermission GlobalPermission { get; init; }
        public DateTime CreationDate { get; private init; }

        public UserEntity(string nickname, Guid accountId, UserType userType = UserType.User, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Nickname = nickname;
            Status = String.Empty;
            IsActive = isActive;
            ImageUri = default;
            AccountId = accountId;
            UserType = userType;
            GlobalPermission = UserGlobalPermission.None;
            CreationDate = DateTime.UtcNow;
        }
    }
}