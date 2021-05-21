using System;
using Chattle.Models;

namespace Chattle.ModelsDto
{
    public record UserDto : IDtoObject
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Nickname { get; init; } = String.Empty;
        public string Status { get; init; } = String.Empty;
        public bool IsActive { get; init; } = true;
        public Uri? ImageUri { get; init; } = null;
        public Guid AccountId { get; init; } = Guid.Empty;
        public UserType UserType { get; init; } = UserType.User;
        public UserGlobalPermission GlobalPermission { get; init; } = UserGlobalPermission.None;
        public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    }
}