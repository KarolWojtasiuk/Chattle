using System;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record UserEntity : IEntity
    {
        public Guid Id { get; init; } = Guid.Empty;
        public string Nickname { get; init; } = String.Empty;
        public string Status { get; init; } = String.Empty;
        public bool IsActive { get; init; } = false;
        public Uri? ImageUri { get; init; } = null;
        public Guid AccountId { get; init; } = Guid.Empty;
        public UserType UserType { get; init; } = UserType.User;
        public UserGlobalPermission GlobalPermission { get; init; } = UserGlobalPermission.None;
        public DateTime CreationDate { get; init; } = DateTime.MinValue;
    }
}