using System;
using Chattle.Models;

namespace Chattle.Database.Entities
{
    public record UserEntity
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
    }
}